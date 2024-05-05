using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class SleepController : MonoBehaviour
{
    [SerializeField] private GameObject sleepCamera;
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private GameObject fadeImageGameObject;

    private Image fadeImage;

    private void Start()
    {
        fadeImage = fadeImageGameObject.GetComponent<Image>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            playerCamera.SetActive(false);
            sleepCamera.SetActive(true);
            fadeImageGameObject.SetActive(true);
            StartFadeIn();
        }
        else
        {
            //Hide();
        }
    }

    private IEnumerator FadeIn()
    {
        Debug.Log("fadein");
        for (float i = 0f; i <= 1; i += .05f)
        {
            Color imageColor = fadeImage.color;
            imageColor.a = i;
            Debug.Log(imageColor.a);
            yield return new WaitForSeconds(5f);
        }
    }

    private IEnumerator FadeOut()
    {
        Debug.Log("fadeout");
        for (float i = 1f; i >= 0f; i -= .05f)
        {
            Color imageColor = fadeImage.color;
            imageColor.a = i;
            Debug.Log(imageColor.a);
            yield return new WaitForSeconds(3f);
        }
    }

    private void StartFadeIn()
    {
        Debug.Log("startfadein");
        StartCoroutine(FadeIn());

        StartFadeOut();
    }

    private void StartFadeOut()
    {
        Debug.Log("startfadeout");
        StartCoroutine (FadeOut());
    }

    private void Hide()
    {
        playerCamera.SetActive(true);
        sleepCamera.SetActive(false);
        fadeImageGameObject.SetActive(false);
    }
}
