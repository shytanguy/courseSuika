using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

[Serializable]
public class SaveData
{

    public long score;

    public event Action<long> LoadDataEvent;
    public void SaveGame()
    {
       YandexGame.savesData.score = score;
        YandexGame.SaveProgress();

    }
   
    public void LoadData()
    {
        YandexGame.LoadCloud();
        score = YandexGame.savesData.score;

        LoadDataEvent?.Invoke(YandexGame.savesData.score);
    }
}
