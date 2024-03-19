using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitBehaviourScript : MonoBehaviour
{

    [SerializeField] private FruitSO _fruitType;


 
    private event Action<FruitSO> FruitChanged;

  [SerializeField]  private SpriteRenderer _spriteRenderer;

   [SerializeField] private Rigidbody2D _rigidBody2d;

    [SerializeField] private PolygonCollider2D _collider;

    [SerializeField] private LayerMask _fruitLayer;

    private bool _merging=false;


    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _rigidBody2d = GetComponent<Rigidbody2D>();

        _collider = GetComponent<PolygonCollider2D>();
    }
    private void Start()
    {
        UpdateFruitSO(_fruitType);

        GravityOn(false);

       
      
    }
    private void OnDisable()
    {
        FruitChanged -= UpdateFruitSO;
    }

    private void OnEnable()
    {
        FruitChanged += UpdateFruitSO;
    }
    public bool GravityOn(bool gravityOn)
    {
        if (gravityOn == false)
        {

            _rigidBody2d.gravityScale = 0;
        }
        else
            _rigidBody2d.gravityScale = _fruitType.GetGravityScale();

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

            _fruitType = _fruitType.GetMergeFruit();

            FruitChanged?.Invoke(_fruitType);
        }

        _merging = false;
    }

    public void UpdateFruitSO(FruitSO fruit)
    {

        transform.localScale = _fruitType.GetSize();

        _spriteRenderer.sprite = _fruitType.GetSprite();

        _rigidBody2d.sharedMaterial = _fruitType.GetPhysicsMaterial2D();

        _rigidBody2d.gravityScale = _fruitType.GetGravityScale();

        _collider.points = _fruitType.GetColliderPoints();
    }

   /* private void OnValidate()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _rigidBody2d = GetComponent<Rigidbody2D>();

        UpdateFruitSO(_fruitType);
    }*/
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
