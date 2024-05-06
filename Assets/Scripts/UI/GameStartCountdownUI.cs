using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;

    private void Start()
    {
        BobaShopGameManager.Instance.OnStateChanged += BobaShopGameManager_OnStateChanged;

        Hide();
    }

    private void Update()
    {
        countdownText.text = Mathf.Ceil(BobaShopGameManager.Instance.GetCountdownToStartTimer()).ToString();
    }

    private void BobaShopGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (BobaShopGameManager.Instance.IsCountdownToStartActive())
        {
            Show();
        }
        else
        {
            Hide();
        }
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
