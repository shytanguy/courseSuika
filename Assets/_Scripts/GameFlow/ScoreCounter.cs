using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

  [SerializeField]  private FruitSpawner _fruitSpawner;

    
    private int _score;

    private void OnEnable()
    {
        _fruitSpawner.NewFruit += AddNewEvent;
        GameFlowScript.instance.GameLost += SaveScore;
      
    }

    private void OnDisable()
    {
        _fruitSpawner.NewFruit -= AddNewEvent;
        GameFlowScript.instance.GameLost -= SaveScore;

    }
    private void AddNewEvent(FruitBehaviourScript fruit)
    {
        fruit.MergeEvent += AddPoints;
    }

    private void AddPoints(FruitSO fruit)
    {
        _score += fruit.GetPoints();


        _scoreText.text = _score.ToString();
    }

 private void SaveScore()
    {
        DatabaseManager.AddOrUpdatePlayerTotalPoints(1, _score);
    }
   
}
