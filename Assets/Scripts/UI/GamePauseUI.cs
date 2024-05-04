using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePauseUI : MonoBehaviour
{
    private void Start()
    {
        BobaShopGameManager.Instance.OnGamePaused += BobaShopGameManager_OnGamePaused;
        BobaShopGameManager.Instance.OnGameUnpaused += BobaShopGameManager_OnGameUnpaused;

        Hide();
    }
    private void BobaShopGameManager_OnGamePaused(object sender, EventArgs e)
    {
        Show();
    }

    private void BobaShopGameManager_OnGameUnpaused(object sender, EventArgs e)
    {
        Hide();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
