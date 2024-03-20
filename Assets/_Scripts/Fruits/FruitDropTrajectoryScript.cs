using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FruitDropTrajectoryScript : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;

    private PolygonCollider2D _polygonCollider;

    [SerializeField] private LayerMask _ToDropAtLayers;

    private float _height;

    private Vector3[] _shapePoints;

    private FruitBehaviourScript _fruitScript;

    private void Awake()
    {
        _polygonCollider = GetComponent<PolygonCollider2D>();

        _fruitScript = GetComponent<FruitBehaviourScript>();

        _lineRenderer = GetComponent<LineRenderer>();
    }
    private void UpdateFruitType(FruitSO fruit)
    {
        _shapePoints = new Vector3[_polygonCollider.points.Length];

        for (int i = 0; i < _polygonCollider.points.Length; i++)
        {
            _shapePoints[i] = _polygonCollider.points[i] * fruit.GetSize()*Vector3.one;
        }

        _lineRenderer.positionCount = _shapePoints.Length;

        _height = _polygonCollider.bounds.size.y;
    }

  
    private void Start()
    {
        UpdateFruitType(_fruitScript.GetFruit());
    }

    private void OnDisable()
    {
        _fruitScript.FruitChanged -= UpdateFruitType;
        _fruitScript.Dropped -= DestroyComponentOnDrop;
    }

    private void OnEnable()
    {
        _fruitScript.FruitChanged += UpdateFruitType;
        _fruitScript.Dropped += DestroyComponentOnDrop;
    }
    private void FixedUpdate()
    {

        RaycastHit2D hitData=  Physics2D.Raycast(transform.position+_height/1.8f*Vector3.down, Vector2.down, 15, _ToDropAtLayers);
        
        if (hitData.collider!=null)
        {
         
            _lineRenderer.SetPositions(ConvertPointsToDropPoint(_shapePoints,hitData.point+_height/2*Vector2.up));
   
        }
    }

    private Vector3[] ConvertPointsToDropPoint(Vector3[] points, Vector3 dropPoint)
    {
        Vector3[] newPoints = new Vector3[points.Length];
       for (int i=0; i < points.Length; i++)
        {
            newPoints[i] +=points[i]+ dropPoint;
        }

        return newPoints;
    }
    private void DestroyComponentOnDrop()
    {
        Destroy(_lineRenderer);

      Destroy(this);
    }
}
