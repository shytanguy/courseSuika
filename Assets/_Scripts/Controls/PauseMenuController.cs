using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenuController : MonoBehaviour
{
    public static event Action<bool> Pause;

    private static bool _paused=false;
    [SerializeField] private  GameObject _pauseMenu;
    [SerializeField] private GameFlowScript _gameFlow;
    public  void PauseWithMenuEvent()
    {
        _paused = !_paused;
        _pauseMenu.SetActive(_paused);
        Pause?.Invoke(_paused);
    }

    public static void PauseEvent()
    {
        _paused = !_paused;
       
        Pause?.Invoke(_paused);
    }

    public static void PauseEvent(bool pause)
    {
        _paused = pause;

        Pause?.Invoke(_paused);
    }


    private void OnEnable()
    {
        _gameFlow.GameLost += GameEnd;
        Pause += PauseGame;
    }

    private void OnDisable()
    {
        _gameFlow.GameLost -= GameEnd;
        Pause -= PauseGame;
    }

    private void GameEnd()
    {
        Pause -= PauseGame;
    }
    private void PauseGame(bool pause)
    {
        if (pause) Time.timeScale = 0;
        else
            Time.timeScale = 1f;
    }

 
}
