using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class AdMenuController : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private RemoveFruitsForAD _ad;
   public void OpenAdMenu()
    {
        if (_ad.CanWatchAd() == false) return;
        _menu.SetActive(true);

        PauseMenuController.PauseEvent(true);
    }
    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += CloseAdMenu;
    }

    private void OnDisable()
    {

        YandexGame.RewardVideoEvent -= CloseAdMenu;
    }
    public void CloseAdMenu(int id)
    {
        _menu.SetActive(false);

        PauseMenuController.PauseEvent(false);
    }

 
   
}
