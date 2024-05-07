using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SleepController : MonoBehaviour
{
    [SerializeField] private GameObject sleepingUI;
    [SerializeField] private GameObject quotaFailedUI;
    [SerializeField] private GameObject quotaPassedUI;

    private void Awake()
    {
        sleepingUI.SetActive(false);
        quotaFailedUI.SetActive(false);
        quotaPassedUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (PlayerStats.Instance.getDaysLeft() == 0 && PlayerStats.Instance.isQuotaMet())
            {
                quotaPassedUI.GetComponent<AudioSource>().Play();
                ShowQuotaPassed(quotaPassedUI);
            }
            else if (PlayerStats.Instance.getDaysLeft() == 0 && !PlayerStats.Instance.isQuotaMet())
            {
                quotaFailedUI.GetComponent<AudioSource>().Play();
                ShowQuotaFailed(quotaFailedUI);
            }
            else
            {
                ShowSleeping(sleepingUI);
            }
        }
    }

    private void ShowSleeping(GameObject uiToShow)
    {
        PlayerStats.Instance.decreaseDaysLeft();
        StartCoroutine(ShowSleepingCoroutine());
    }

    private void ShowQuotaFailed(GameObject uiToShow)
    {
        PlayerStats.Instance.resetDaysLeft();
        PlayerStats.Instance.resetPlayersProfit();
        PlayerStats.Instance.resetQuota();
        StartCoroutine(ShowQuotaFailedCoroutine());
    }

    private void ShowQuotaPassed(GameObject uiToShow)
    {
        PlayerStats.Instance.resetDaysLeft();
        PlayerStats.Instance.updateQuota();
        StartCoroutine(ShowQuotaPassedCoroutine());
    }

    private IEnumerator ShowSleepingCoroutine()
    {
        sleepingUI.SetActive(true);

        yield return new WaitForSeconds(3f);

        Hide(sleepingUI);
    }

    private IEnumerator ShowQuotaFailedCoroutine()
    {
        quotaFailedUI.SetActive(true);

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(0);
    }

    private IEnumerator ShowQuotaPassedCoroutine()
    {
        quotaPassedUI.SetActive(true);

        yield return new WaitForSeconds(3f);

        Hide(quotaPassedUI);
    }

    private void Hide(GameObject uiToHide)
    {
        uiToHide.SetActive(false);
    }
}
