using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public event Action<bool> Pause;

    private bool _paused=false;
    [SerializeField] private GameObject _pauseMenu;
    public void PauseEvent()
    {
        _paused = !_paused;
        Pause?.Invoke(_paused);
    }
    private void Start()
    {
        Pause += PauseMenuSetUp;
    }
    private void PauseMenuSetUp(bool pause)
    {
        _pauseMenu.SetActive(pause);

        if (pause) Time.timeScale = 0;
        else
            Time.timeScale = 1f;
    }
}
