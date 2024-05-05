using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance {get; private set;}

    [SerializeField] private TextMeshProUGUI playersProfitText;
    [SerializeField] private TextMeshProUGUI quotaText;
    [SerializeField] private TextMeshProUGUI daysLeftText;

    public static int daysLeft = 2; // plays in sets of 2 days
    public static int playersProfit = 0;
    public static int quota = 40;

    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            setPlayersProfitText();
            setQuotaText();
            setDaysLeftText();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void updatePlayersProfit(int amountToAdd)
    {
        playersProfit += amountToAdd;
        Debug.Log("set players profit to: " + playersProfit);
        setPlayersProfitText();
    }

    public int getPlayersProfit()
    {
        return playersProfit;
    }

    public void setNewQuota(int newQuota)
    {
        quota += newQuota;
        setQuotaText();
    }

    public int getNewQuota()
    {
        return quota;
    }

    public void decreaseDaysLeft()
    {
        daysLeft--;

        if (daysLeft == 0)
        {
            Debug.Log("days left = 0");
        }

        setDaysLeftText();
    }

    public int getDaysLeft()
    {
        return daysLeft;
    }

    public void setPlayersProfitText()
    {
        playersProfitText.text = "profit made: " + playersProfit;
    }
    public void setQuotaText()
    {
        quotaText.text = "quota: " + quota;
    }

    public void setDaysLeftText()
    {
        daysLeftText.text = "days left: " + daysLeft;
    }
}
