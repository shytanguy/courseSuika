using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.CinemachineFreeLook;

public class FruitBehaviourScript : MonoBehaviour
{

    [SerializeField] private FruitSO _fruitType;


 
   

  [SerializeField]  private SpriteRenderer _spriteRenderer;

   [SerializeField] private Rigidbody2D _rigidBody2d;

    [SerializeField] private PolygonCollider2D _collider;

    [SerializeField] private LayerMask _fruitLayer;

    [SerializeField] private LayerMask _allColliders;

    private bool _merging=false;

    public event Action Dropped;

    public event Action<FruitSO> FruitChanged;

    public event Action<FruitSO> MergeEvent;

    [SerializeField] private float _growSpeed = 0.05f;
    [SerializeField] private float _pushForce = 1f;
    [SerializeField] private float _selfUpForce = 1f;
    private Vector3 _initScale;

    [SerializeField] private GameObject _effect;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _rigidBody2d = GetComponent<Rigidbody2D>();

        _collider = GetComponent<PolygonCollider2D>();

        _initScale = transform.localScale;
    }

    public Vector2 GetExdendsOfCollider()
    {
        return _collider.bounds.extents;
    }

    private void FixedUpdate()
    {
        if (transform.localScale != _initScale.x*(Vector3)_fruitType.GetSize())
        {
            CheckCollisions(_collider.bounds.extents*2.5f, transform.position);

            transform.localScale += _growSpeed* _initScale.x * (Vector3)_fruitType.GetSize();

            if (transform.localScale.x > _initScale.x * _fruitType.GetSize().x)
            {
                transform.localScale = _initScale.x * _fruitType.GetSize();
            }
        }
    }

    private void CheckCollisions(Vector2 scale, Vector3 position)
    {
   
     AddForcesToOverlapFruits(  Physics2D.OverlapCircleAll(position, scale.x, _allColliders),position);
    }

    private void AddForcesToOverlapFruits(Collider2D[] colliders, Vector3 position)
    {
        foreach(var collider in colliders)
        {
            _rigidBody2d.AddForce(_selfUpForce* Vector3.up, ForceMode2D.Force);
            collider.attachedRigidbody?.AddForce((_pushForce*Vector3.up), ForceMode2D.Force);
        }
    }
    private void Start()
    {
      
        transform.localScale = Vector3.zero;

        _collider.enabled = false;
    }

    public bool GravityOn(bool gravityOn)
    {
        if (gravityOn == false)
        {

            _rigidBody2d.gravityScale = 0;
        }
        else
        {
            _rigidBody2d.totalForce = Vector2.zero;
            _rigidBody2d.velocity = Vector2.zero;
            _rigidBody2d.gravityScale = _fruitType.GetGravityScale();
            _collider.enabled =true;
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
           fruit.CollapseFruit();

            UpdateFruitSO(_fruitType.GetMergeFruit());

            Instantiate(_effect, transform.position, transform.rotation);

            MergeEvent?.Invoke(_fruitType);

            transform.localScale = Vector3.zero;
        }

        _merging = false;
    }
    public void CollapseFruit()
    {
        this.enabled = false;

        gameObject.layer = 0;

        Destroy(gameObject);
    }
    public void UpdateFruitSO(FruitSO fruit)
    {
        _fruitType = fruit;

       // transform.localScale = _initScale.x * _fruitType.GetSize();

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
