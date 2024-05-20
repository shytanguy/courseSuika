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

    private float _width;

    private Vector3[] _shapePoints;

    private FruitBehaviourScript _fruitScript;

    private Vector3 _initScale;

    private void Awake()
    {
        _polygonCollider = GetComponent<PolygonCollider2D>();

        _fruitScript = GetComponent<FruitBehaviourScript>();

        _lineRenderer = GetComponent<LineRenderer>();

        _initScale = transform.localScale;

        _lineRenderer.positionCount =2;
    }
    
    private void OnDisable()
    {
     
        _fruitScript.Dropped -= DestroyComponentOnDrop;
    }

    private void OnEnable()
    {
       
        _fruitScript.Dropped += DestroyComponentOnDrop;
    }
    private void FixedUpdate()
    {
        
        RaycastHit2D hitData=  Physics2D.Raycast(transform.position+_height/1.8f*Vector3.down, Vector2.down, 15, _ToDropAtLayers);
        
        if (hitData.collider!=null)
        {
         
            _lineRenderer.SetPosition(0, transform.position);

            _lineRenderer.SetPosition(1, hitData.point);

        }
    }

    
    private void DestroyComponentOnDrop()
    {
        Destroy(_lineRenderer);

      Destroy(this);
    }
}
