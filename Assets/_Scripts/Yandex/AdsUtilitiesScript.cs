using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public static class AdsUtilitiesScript
{
   public static void TryShowFullScreenAdd()
    {
        YandexGame.FullscreenShow();
    }

    public static void TryShowFullScreenAdd(int chance)
    {
        int random = Random.Range(0, 100);

        if (chance < random)
        {
            return;
        }
        else
            YandexGame.FullscreenShow();
    }
}
