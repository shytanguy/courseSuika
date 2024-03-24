using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextFruitIcon : MonoBehaviour
{
    [SerializeField] private FruitSpawner _fruitSpawner;

    [SerializeField] private Image _nextFruitIcon;


    private void OnEnable()
    {

        _fruitSpawner.NextFruit += ChangeIcon;

    }

    private void ChangeIcon(FruitSO fruit)
    {
        _nextFruitIcon.sprite = fruit.GetSprite();
    }
}
