using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowSounds : MonoBehaviour
{
    [SerializeField] private AudioClip _TimerSound;

    [SerializeField] private AudioClip _LostSound;

    private GameFlowScript _gameFlowScript;

    private float _timer;

    private void Awake()
    {
        _gameFlowScript = GetComponent<GameFlowScript>();

     
    }

    private void OnEnable()
    {
        _gameFlowScript.TimeRemaining += PlayTimerSound;

        _gameFlowScript.GameLost += PlayLostSound;
    }

    private void OnDisable()
    {
        _gameFlowScript.TimeRemaining -= PlayTimerSound;

        _gameFlowScript.GameLost -= PlayLostSound;
    }
    private void PlayTimerSound(float time, bool active)
    {
        if (!active) return;

        else if (Time.time-_timer>1)
        {
            AudioManager.audioManager.PlaySound(_TimerSound);

            _timer = Time.time;
        }
    }
    private void PlayLostSound()
    {
        AudioManager.audioManager.PlaySound(_LostSound);
    }

}
