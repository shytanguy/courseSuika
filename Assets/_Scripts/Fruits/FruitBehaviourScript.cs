using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitBehaviourScript : MonoBehaviour
{

    [SerializeField] private FruitSO _fruitType;


 
   

  [SerializeField]  private SpriteRenderer _spriteRenderer;

   [SerializeField] private Rigidbody2D _rigidBody2d;

    [SerializeField] private PolygonCollider2D _collider;

    [SerializeField] private LayerMask _fruitLayer;

    private bool _merging=false;

    public event Action Dropped;

    public event Action<FruitSO> FruitChanged;

    public event Action MergeEvent;

    [SerializeField] private float _growSpeed = 0.05f;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _rigidBody2d = GetComponent<Rigidbody2D>();

        _collider = GetComponent<PolygonCollider2D>();

    }

    public Vector2 GetExdendsOfCollider()
    {
        return _collider.bounds.extents;
    }

    private void FixedUpdate()
    {
        if (transform.localScale != (Vector3)_fruitType.GetSize())
        {
            transform.localScale += _growSpeed*(Vector3)_fruitType.GetSize();

            if (transform.localScale.x > _fruitType.GetSize().x)
            {
                transform.localScale = _fruitType.GetSize();
            }
        }
    }
    private void Start()
    {
      
        transform.localScale = Vector3.zero;
    }

    public bool GravityOn(bool gravityOn)
    {
        if (gravityOn == false)
        {

            _rigidBody2d.gravityScale = 0;
        }
        else
        {
            _rigidBody2d.gravityScale = _fruitType.GetGravityScale();

            Dropped?.Invoke();

        }
       
        return gravityOn;
    }
    public bool GetMergeStatus()
    {
        return _merging;
    }
    public void Merge(FruitBehaviourScript fruit)
    {
        if (fruit.GetMergeStatus()) return;

        _merging = true;

        if (fruit.GetFruit() == _fruitType)
        {
            fruit.enabled = false;

            fruit.gameObject.layer = 0;

            Destroy(fruit.gameObject);

            UpdateFruitSO(_fruitType.GetMergeFruit());

            MergeEvent?.Invoke();

            transform.localScale = Vector3.zero;
        }

        _merging = false;
    }

    public void UpdateFruitSO(FruitSO fruit)
    {
        _fruitType = fruit;

        transform.localScale = _fruitType.GetSize();

        _spriteRenderer.sprite = _fruitType.GetSprite();

        _rigidBody2d.sharedMaterial = _fruitType.GetPhysicsMaterial2D();

        if (_rigidBody2d.gravityScale!=0)
        _rigidBody2d.gravityScale = _fruitType.GetGravityScale();

        _collider.points = _fruitType.GetColliderPoints();

        FruitChanged?.Invoke(_fruitType);
    }


    public FruitSO GetFruit()
    {
        return _fruitType;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((_fruitLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            FruitBehaviourScript otherFruit;
           
            if( collision.gameObject.TryGetComponent<FruitBehaviourScript>(out otherFruit))
            {        
                Merge(otherFruit);
            }
        }
    }
    
}
