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

    [SerializeField] private int _Id;
    public float GetGravityScale()
    {
        return _gravityScale;
    }
    public Vector2[] GetColliderPoints()
    {
        Vector2[] points=new Vector2[_colliderPoints.Length];
        for(int i = 0; i < _colliderPoints.Length; i++)
        {
            points[i] = _colliderPoints[i];
        }
        return points;
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

    private void OnEnable()
    {
        try
        {
            _pointsForMerging = DatabaseManager.GetFruitPointsById(_Id);
            Debug.Log("assigned points to fruit succesfully");
        }
        catch
        {
            Debug.Log("could not assign points");
        }
    }
}
