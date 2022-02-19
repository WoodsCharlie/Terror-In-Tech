using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopControl : MonoBehaviour
{
    int currency;
    int speedCost;
    int healthCost;

    public Text currencyText;
    public Text speedText;
    public Text healthText;

    public Button speedButton;
    public Button healthButton;

    // Start is called before the first frame update
    void Start()
    {
        currency = PlayerPrefs.GetInt("currency");
        if (PlayerPrefs.GetInt("speedCost") == 0)
            PlayerPrefs.SetInt("speedCost", 5);
        if (PlayerPrefs.GetInt("healthCost") == 0)
            PlayerPrefs.SetInt("healthCost", 5);
    }

    // Update is called once per frame
    void Update()
    {
        speedCost = PlayerPrefs.GetInt("speedCost");
        healthCost = PlayerPrefs.GetInt("healthCost");

        currencyText.text = "bank: $" + currency.ToString();
        speedText.text = speedCost.ToString() + ".";
        healthText.text = healthCost.ToString() + ".";

        if (currency >= speedCost)
            speedButton.interactable = true;
        else
            speedButton.interactable = false;
        if (currency >= healthCost)
            healthButton.interactable = true;
        else
            healthButton.interactable = false;
    }

    public void buySpeed()
    {
        currency -= speedCost;
        PlayerPrefs.SetInt("speedCost", speedCost+5);
        PlayerPrefs.SetInt("speed", PlayerPrefs.GetInt("speed") + 1);
    }

    public void buyhealth()
    {
        currency -= healthCost;
        PlayerPrefs.SetInt("healthCost", healthCost + 5);
        PlayerPrefs.SetInt("health", PlayerPrefs.GetInt("health") + 1);
    }

    public void exitShop()
    {
        PlayerPrefs.SetInt("currency", currency);
        SceneManager.LoadScene("SampleScene");
    }
}
