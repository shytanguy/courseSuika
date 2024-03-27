using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class GameOverScreen : MonoBehaviour
{

    [SerializeField] private GameObject _gameOverScreen;

  [SerializeField]  private GameFlowScript _gameFlow;
    public void RestartGame()
    {
       

        AdsUtilitiesScript.TryShowFullScreenAdd();
     
    }
    private void RestartGameAfterAd()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }
    private void OnEnable()
    {
        YandexGame.CloseFullAdEvent += RestartGameAfterAd;
        YandexGame.ErrorFullAdEvent += RestartGameAfterAd;
        _gameFlow.GameLost += SetGameOverScreen;
    }
    private void OnDisable()
    {
        YandexGame.CloseFullAdEvent -= RestartGameAfterAd;
        YandexGame.ErrorFullAdEvent -= RestartGameAfterAd;
        _gameFlow.GameLost -= SetGameOverScreen;
    }

    private void SetGameOverScreen()
    {
        Time.timeScale = 0;
        Cursor.visible = true;

       
        _gameOverScreen.SetActive(true);
    
    }

  
}