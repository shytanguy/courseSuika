using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FruitSpawner : MonoBehaviour
{
    [SerializeField] private MouseControls _mouseControls;

    [SerializeField] private FruitBehaviourScript _fruitPrefab;

    [SerializeField] private FruitSO[] _fruitsScriptableObjectsToSpawn;

    private FruitBehaviourScript _CurrentFruit;

    [SerializeField] private float _YCoordinateSpawn;

    [SerializeField] private Vector2 _AllowedXCoords;

    private float _fruitBoundsX;

    public event Action<FruitBehaviourScript> NewFruit;

    public event Action<FruitSO> NextFruit;

    private FruitSO _nextFruit;


    private void OnEnable()
    {
        _mouseControls.PositionClicked += DropFruit;

        _mouseControls.MouseHoverPositionChanged += MoveFruit;
    }
    private void OnDisable()
    {
        _mouseControls.PositionClicked -= DropFruit;

        _mouseControls.MouseHoverPositionChanged -= MoveFruit;
    }
    private void MoveFruit(Vector3 position)
    {
        if (_CurrentFruit == null) return;

        float XCoordinate =Mathf.Clamp(  position.x,_AllowedXCoords.x+_fruitBoundsX/2,_AllowedXCoords.y-_fruitBoundsX/2);

        _CurrentFruit.transform.position = new Vector3(XCoordinate, _YCoordinateSpawn);


    }
    private void Start()
    {
      

       _nextFruit= _fruitsScriptableObjectsToSpawn[UnityEngine.Random.Range(0, _fruitsScriptableObjectsToSpawn.Length)];

        ChangeFruit();
    }

    private void DropFruit(Vector3 DropPosition)
    {
        MoveFruit(DropPosition);
     
        if (_CurrentFruit != null)
        {

            _CurrentFruit.GravityOn(true);


            _CurrentFruit = null;


            StartCoroutine(ChangeFruitDelay());
        }
    }
  
    private void ChangeFruit()
    {

        _CurrentFruit = Instantiate(_fruitPrefab, new Vector3(0, _YCoordinateSpawn, 0), transform.rotation);

        _CurrentFruit.UpdateFruitSO(_nextFruit);

        _nextFruit = _fruitsScriptableObjectsToSpawn[UnityEngine.Random.Range(0, _fruitsScriptableObjectsToSpawn.Length)];

        NextFruit?.Invoke(_nextFruit);

        NewFruit?.Invoke(_CurrentFruit);

        Vector2[] points = new Vector2[_CurrentFruit.GetFruit().GetColliderPoints().Length];

        points = _CurrentFruit.GetFruit().GetColliderPoints();

        float xMin=0;

        float xMax = 0;

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = points[i] * _CurrentFruit.GetFruit().GetSize().x * _fruitPrefab.transform.localScale.x;

            if (xMax < points[i].x) xMax = points[i].x;

            if (xMin > points[i].x) xMin = points[i].x;
        }

        _fruitBoundsX = xMax -xMin;
    }

    private IEnumerator ChangeFruitDelay()
    {
        if (_CurrentFruit != null) yield break;
        yield return new WaitForSeconds(0.5f);

        ChangeFruit();
    }
}
