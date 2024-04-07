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
    public void WatchAd()
    {
        if (Time.time - _Timer >= _TimeBeforeAd)
        {

            AdsUtilitiesScript.ShowRewardedAd(_RewardId);

            _Timer = Time.time;

        }
    }

    public void Update()
    {
        if (_timerText!=null)
        _timerText.text = (_TimeBeforeAd - Time.time - _Timer).ToString("0");
    }

}
