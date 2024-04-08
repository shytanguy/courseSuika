using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class RemoveFruitsForAD : MonoBehaviour
{
    [SerializeField] private float _TimeBeforeAd=60;

    private float _Timer;

   [SerializeField] private FruitsContainer _fruitsContainer;

    [SerializeField] private TextMeshProUGUI _timerText;

    [SerializeField] private int _RewardId=1;

    private void OnEnable()
    {
        YandexGame.RewardVideoEvent+= RemoveFruits;

      
    }

    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= RemoveFruits;
    }

    private void RemoveFruits(int id)
    {
        if (_RewardId == id)
        {
            _fruitsContainer.RemoveAllFruits();
        }
    }
    public bool CanWatchAd()
    {
        return (Time.time - _Timer >= _TimeBeforeAd);
    }
    public void WatchAd()
    {
        if (Time.time - _Timer >= _TimeBeforeAd)
        {
            _Timer = Time.time;

            AdsUtilitiesScript.ShowRewardedAd(_RewardId);

            

        }
    }

    public void Update()
    {
        if (_timerText != null)
        { if ((_TimeBeforeAd - (Time.time - _Timer)) > 0)
                _timerText.text = (_TimeBeforeAd - (Time.time - _Timer)).ToString("0");
            else _timerText.text = "";
        }
    }

}
