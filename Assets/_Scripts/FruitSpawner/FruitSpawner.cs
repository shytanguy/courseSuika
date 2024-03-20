using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FruitSpawner : MonoBehaviour
{
    [SerializeField] private MouseControls _mouseControls;

    [SerializeField] private FruitBehaviourScript _fruitPrefab;

    [SerializeField] private FruitSO[] _fruitsScriptableObjectsToSpawn;

    private FruitBehaviourScript _CurrentFruit;

    [SerializeField] private float _YCoordinateSpawn;

    [SerializeField] private Vector2 _AllowedXCoords;

    private float _fruitBoundsX;
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

 
    private void DropFruit(Vector3 DropPosition)
    {
        MoveFruit(DropPosition);

        if (_CurrentFruit!=null)

        _CurrentFruit.GravityOn(true);

        _CurrentFruit = null;

        StartCoroutine(ChangeFruitDelay());
    }

    private void ChangeFruit()
    {

        _CurrentFruit = Instantiate(_fruitPrefab, new Vector3(0, _YCoordinateSpawn, 0), Quaternion.identity);


        _CurrentFruit.UpdateFruitSO(_fruitsScriptableObjectsToSpawn[Random.Range(0, _fruitsScriptableObjectsToSpawn.Length)]);

        _fruitBoundsX = _CurrentFruit.GetExdendsOfCollider().x;
    }

    private IEnumerator ChangeFruitDelay()
    {
        yield return new WaitForSeconds(0.5f);

        ChangeFruit();
    }
}
