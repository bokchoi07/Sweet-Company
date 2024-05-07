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

    private AudioSource buttonPressedSFX;

    private void Start()
    {
        buttonPressedSFX = GetComponent<AudioSource>();
    }

    private void Awake()
    {
        playButton.onClick.AddListener(() => // lamba expression
        {
            Loader.Load(Loader.Scene.Office);
        });
        howToPlayButton.onClick.AddListener(() =>
        {
            PlayButtonPressedSFX();
            ShowHowToPlayPage();
        });
        quitButton.onClick.AddListener(() => 
        {
            Application.Quit();
        });
        backButton.onClick.AddListener(() =>
        {
            PlayButtonPressedSFX();
            HideHowToPlayPage();
        });

        Time.timeScale = 1f; // resetting time
    }

    private void PlayButtonPressedSFX()
    {
        buttonPressedSFX.Play();
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
