using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FruitsContainer : MonoBehaviour
{

    private List<GameObject> _currentFruits = new List<GameObject>();

    [SerializeField] private FruitSpawner _fruitSpawner;
    private void OnEnable()
    {
        _fruitSpawner.NewFruit += AddFruitToList;
    }
    private void OnDisable()
    {
        _fruitSpawner.NewFruit -= AddFruitToList;
    }
    private void AddFruitToList(FruitBehaviourScript fruitBehaviour)
    {
        _currentFruits.Add(fruitBehaviour.gameObject);

   
    }

    private void RemoveFruitFromList(FruitBehaviourScript fruitBehaviour)
    {
        if (_currentFruits.Contains(fruitBehaviour.gameObject) && fruitBehaviour.gameObject.IsDestroyed())
        {
            _currentFruits.Remove(fruitBehaviour.gameObject);
        }
    }

    public void RemoveAllFruits()
    {
        foreach(GameObject fruit in _currentFruits)
        {
            Destroy(fruit);
            
        }

        _currentFruits.Clear();
    }
}
