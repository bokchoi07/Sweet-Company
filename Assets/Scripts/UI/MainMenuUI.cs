using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button howToPlayButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button backButton;

    [SerializeField] private GameObject howToPlayPage;


    private void Awake()
    {
        playButton.onClick.AddListener(() => // lamba expression
        {
            Loader.Load(Loader.Scene.BobaShop);
        });
        howToPlayButton.onClick.AddListener(() =>
        {
            ShowHowToPlayPage();
        });
        quitButton.onClick.AddListener(() => 
        {
            Application.Quit();
        });
        backButton.onClick.AddListener(() =>
        {
            HideHowToPlayPage();
        });
    }

    private void ShowHowToPlayPage()
    {
        howToPlayPage.SetActive(true);
    }

    private void HideHowToPlayPage()
    {
        howToPlayPage.SetActive(false);
    }
}
