using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="Fruit object data", menuName = "Scriptable Objects/Fruit object data")]
public class FruitSO : ScriptableObject
{
   [SerializeField] private string _name;

   [SerializeField] private Sprite _icon;

   [SerializeField] private int _pointsForMerging;

   [SerializeField] private FruitSO _MergeInto;

   [SerializeField] private Vector2 _size;

    [SerializeField] private PhysicsMaterial2D _physicsMaterial;

    [SerializeField] private Vector2[] _colliderPoints;

    [SerializeField] private float _gravityScale;

    public float GetGravityScale()
    {
        return _gravityScale;
    }
    public Vector2[] GetColliderPoints()
    {
        return _colliderPoints;
    }

    public PhysicsMaterial2D GetPhysicsMaterial2D()
    {
        return _physicsMaterial;
    }
    public string GetName()
    {
        return _name;
    }

    public Vector2 GetSize()
    {
        return _size;
    }
    public Sprite GetSprite()
    {
        return _icon;
    }

    public int GetPoints()
    {
       return _pointsForMerging;
    }

    public FruitSO GetMergeFruit()
    {
        return _MergeInto;
    }
}
