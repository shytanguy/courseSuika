using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlowScript : MonoBehaviour
{
    [SerializeField] private LayerMask _FruitLayers;

    [SerializeField] private float _timeToLose;

    private bool _FruitInDangerZone;

    private float _timer;

    public event Action<float,bool> TimeRemaining;

    public event Action GameLost;

    private List<GameObject> _lostFruits=new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((_FruitLayers.value & (1 << collision.gameObject.layer)) > 0)
        {
            if (_lostFruits.Exists(i=>i==collision.gameObject))
            _lostFruits.Remove(collision.gameObject);

            if (_lostFruits.Count == 0)
            {
                _FruitInDangerZone = false;

                TimeRemaining?.Invoke(_timeToLose - (Time.time - _timer), _FruitInDangerZone);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((_FruitLayers.value & (1 << collision.gameObject.layer)) > 0&&gameObject.activeInHierarchy)
        {
            _timer = Time.time;

            if (collision.gameObject.activeInHierarchy)
            _lostFruits.Add( collision.gameObject);

            _FruitInDangerZone = true;
        }
    }
    private void Update()
    {
        if (_FruitInDangerZone)
        {
            foreach(GameObject gameObj in _lostFruits)
            {
                if (gameObj == null) _lostFruits.Remove(gameObj);
            }
            if (_lostFruits.Count == 0) _FruitInDangerZone = false;
            TimeRemaining?.Invoke(_timeToLose-(Time.time-_timer), _FruitInDangerZone);

            if (Time.time - _timer>=_timeToLose)
            {
                _FruitInDangerZone = false;

                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);

            }
        }
       
           

    }
    private IEnumerator LosingTimer()
    {
        if (_FruitInDangerZone) yield break;

        _FruitInDangerZone = true;
        yield return new WaitForSeconds(_timeToLose);
        
        if (_FruitInDangerZone == true)
        {
            _FruitInDangerZone = false;

            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);

            GameLost?.Invoke();

            StopAllCoroutines();
        }
    }
}
