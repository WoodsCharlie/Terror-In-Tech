using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopControl : MonoBehaviour
{
    int currency = 0;
    int speedCost = 0;
    int healthCost = 0;
    int ianHealthCost = 0;

    public Text currencyText;
    public Text speedText;
    public Text healthText;
    public Text ianHealthText;

    public Button speedButton;
    public Button healthButton;
    public Button ianHealthButton;

    public Button orange;
    public Button yellow;
    public Button green;
    public Button blue;
    public Button purple;

    // Start is called before the first frame update
    void Start()
    {
        currency = PlayerPrefs.GetInt("currency");
        if (PlayerPrefs.GetInt("speedCost") == 0)
            PlayerPrefs.SetInt("speedCost", 5);
        if (PlayerPrefs.GetInt("healthCost") == 0)
            PlayerPrefs.SetInt("healthCost", 5);
        if (PlayerPrefs.GetInt("ianHealthCost") == 0)
            PlayerPrefs.SetInt("ianHealthCost", 20);
    }

    // Update is called once per frame
    void Update()
    {
        speedCost = PlayerPrefs.GetInt("speedCost");
        healthCost = PlayerPrefs.GetInt("healthCost");
        ianHealthCost = PlayerPrefs.GetInt("ianHealthCost");

        currencyText.text = "bank: $" + currency.ToString();
        speedText.text = "$" + speedCost.ToString();
        healthText.text = "$" + healthCost.ToString();
        ianHealthText.text = "$" + ianHealthCost.ToString();

        if (currency >= speedCost)
            speedButton.interactable = true;
        else
            speedButton.interactable = false;
        if (currency >= healthCost)
            healthButton.interactable = true;
        else
            healthButton.interactable = false;
        if (currency >= ianHealthCost)
            ianHealthButton.interactable = true;
        else
            ianHealthButton.interactable = false;

        if (currency >= 5)
            orange.interactable = true;
        else
            orange.interactable = false;
        if (currency >= 10)
            yellow.interactable = true;
        else
            yellow.interactable = false;
        if (currency >= 15)
            green.interactable = true;
        else
            green.interactable = false;
        if (currency >= 20)
            blue.interactable = true;
        else
            blue.interactable = false;
        if (currency >= 25)
            purple.interactable = true;
        else
            purple.interactable = false;

        if (Input.GetKey(KeyCode.F))
        {
            SceneManager.LoadScene("SampleScene");
        }
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
        PlayerPrefs.SetInt("health", PlayerPrefs.GetInt("total health"));
    }

    public void buyIanHealth()
    {
        currency -= healthCost;
        PlayerPrefs.SetInt("ianHealthCost", ianHealthCost + 20);
        PlayerPrefs.SetInt("ianHealth", PlayerPrefs.GetInt("total ianHealth"));
    }

    public void buyOrange()
    {
        currency -= 5;
        PlayerPrefs.SetInt("orange", PlayerPrefs.GetInt("orange") + 20);
    }

    public void buyYellow()
    {
        currency -= 10;
        PlayerPrefs.SetInt("yellow", PlayerPrefs.GetInt("yellow") + 20);
    }

    public void buyGreen()
    {
        currency -= 15;
        PlayerPrefs.SetInt("green", PlayerPrefs.GetInt("green") + 20);
    }

    public void buyBlue()
    {
        currency -= 20;
        PlayerPrefs.SetInt("blue", PlayerPrefs.GetInt("blue") + 20);
    }

    public void buyPurple()
    {
        currency -= 25;
        PlayerPrefs.SetInt("purple", PlayerPrefs.GetInt("purple") + 20);
    }

    public void exitShop()
    {
        PlayerPrefs.SetInt("currency", currency);
        SceneManager.LoadScene("SampleScene");
    }
}
