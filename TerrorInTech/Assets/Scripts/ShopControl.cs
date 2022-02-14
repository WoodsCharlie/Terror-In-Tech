using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopControl : MonoBehaviour
{
    int currency;
    int speedCost;
    int powerCost;

    public Text currencyText;
    public Text speedText;
    public Text powerText;

    public Button speedButton;
    public Button powerButton;

    // Start is called before the first frame update
    void Start()
    {
        currency = PlayerPrefs.GetInt("currency");
        if (PlayerPrefs.GetInt("speedCost") == 0)
            PlayerPrefs.SetInt("speedCost", 5);
        if (PlayerPrefs.GetInt("powerCost") == 0)
            PlayerPrefs.SetInt("powerCost", 5);
    }

    // Update is called once per frame
    void Update()
    {
        speedCost = PlayerPrefs.GetInt("speedCost");
        powerCost = PlayerPrefs.GetInt("powerCost");

        currencyText.text = "bank: $" + currency.ToString();
        speedText.text = speedCost.ToString() + ".";
        powerText.text = powerCost.ToString() + ".";

        if (currency >= speedCost)
            speedButton.interactable = true;
        else
            speedButton.interactable = false;
        if (currency >= powerCost)
            powerButton.interactable = true;
        else
            powerButton.interactable = false;
    }

    public void buySpeed()
    {
        currency -= speedCost;
        PlayerPrefs.SetInt("speedCost", speedCost+5);
        PlayerPrefs.SetInt("speed", PlayerPrefs.GetInt("speed") + 1);
    }

    public void buyPower()
    {
        currency -= powerCost;
        PlayerPrefs.SetInt("powerCost", powerCost + 5);
        PlayerPrefs.SetInt("power", PlayerPrefs.GetInt("power") + 1);
    }

    public void exitShop()
    {
        PlayerPrefs.SetInt("currency", currency);
        SceneManager.LoadScene("SampleScene");
    }
}
