using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{

    [SerializeField] private GameObject _gameOverScreen;

  [SerializeField]  private GameFlowScript _gameFlow;
    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    private void OnEnable()
    {
        _gameFlow.GameLost += SetGameOverScreen;
    }
    private void OnDisable()
    {
        _gameFlow.GameLost -= SetGameOverScreen;
    }

    private void SetGameOverScreen() 
    {
        _gameOverScreen.SetActive(true);
        Time.timeScale = 0;
    }

  
}