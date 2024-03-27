using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlowScript : MonoBehaviour
{

    [HideInInspector] public static GameFlowScript instance;
    [SerializeField] private float _timeToLose;

    public float yCoordinate;

    private  float _timer;

    public  event Action<float,bool> TimeRemaining;

    public  event Action GameLost;


    private  bool _fruitsInDanger;
    private List<Transform> _fruitsLeftOut=new List<Transform>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
    private void Start()
    {
        _fruitsLeftOut.Clear();
    }
    public  void AddDanger(Transform fruit)
    {
        if (_fruitsLeftOut.Count == 0) _timer = Time.time;
        _fruitsLeftOut.Add(fruit);
        _fruitsInDanger = true;
     
    }
    public  void RemoveDanger(Transform fruit)
    {
       
        _fruitsLeftOut.Remove(fruit);

        if (_fruitsLeftOut.Count == 0)
        {
            _fruitsInDanger = false;

            TimeRemaining?.Invoke(_timeToLose - (Time.time - _timer), _fruitsInDanger);
        } 

    }
    private  void FixedUpdate()
    {
        if (_fruitsLeftOut.Count>0)
        {
                
            TimeRemaining?.Invoke(_timeToLose-(Time.time-_timer), _fruitsInDanger);

            if (Time.time - _timer>=_timeToLose)
            {

                _fruitsLeftOut.Clear();

                GameLost?.Invoke();
              

            }
        }
      

    }
  
}
