using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DayOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI completedOrdersText;
    [SerializeField] private TextMeshProUGUI wrongOrdersText;
    [SerializeField] private TextMeshProUGUI profitMadeText;

    private void Start()
    {
        BobaShopGameManager.Instance.OnStateChanged += BobaShopGameManager_OnStateChanged;

        Hide();
    }

    private void BobaShopGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (BobaShopGameManager.Instance.IsDayOver())
        {
            Show();
            int completedOrders = DeliveryManager.Instance.GetCompletedOrdersAmount();
            int wrongOrders = DeliveryManager.Instance.GetWrongOrdersAmount();
            int profit = (15 * completedOrders) - (5 * wrongOrders);

            completedOrdersText.text = completedOrdersText.text + " " + completedOrders;
            wrongOrdersText.text = wrongOrdersText.text + " " + wrongOrders;
            profitMadeText.text = profitMadeText.text + " " + profit;
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
