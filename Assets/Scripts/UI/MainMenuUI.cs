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

    private void Awake()
    {
        playButton.onClick.AddListener(() => // lamba expression
        {
            Loader.Load(Loader.Scene.BobaShop);
        });
        howToPlayButton.onClick.AddListener(() =>
        {
            ShowInstructions();
        });
        quitButton.onClick.AddListener(() => 
        {
            Application.Quit();
        });

    }

    private void ShowInstructions()
    {

    }
}
