using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

  [SerializeField]  private FruitSpawner _fruitSpawner;

    [SerializeField] private GameFlowScript gameFlow;

    private int _score;

    private void OnEnable()
    {
        _fruitSpawner.NewFruit += AddNewEvent;
        gameFlow.GameLost += AddScore;
    }

    private void OnDisable()
    {
        _fruitSpawner.NewFruit -= AddNewEvent;
        gameFlow.GameLost -= AddScore;
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

    public void AddScore()
    {
        if (_score > YandexGame.savesData.score)
        {
            YandexGame.savesData.score = _score;
            YandexGame.SaveProgress();
            
            YandexGame.NewLeaderboardScores("records", _score);
        }
    }
   
}
