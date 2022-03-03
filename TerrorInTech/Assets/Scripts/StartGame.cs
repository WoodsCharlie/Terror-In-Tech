using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public Text survivalText;
    public void startGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetFloat("player y", 9.5f);
        PlayerPrefs.SetFloat("player x", 0.0f);
        SceneManager.LoadScene("SampleScene");
    }

    private void FixedUpdate()
    {
        survivalText.text = "you and ian\nsurvived " + PlayerPrefs.GetInt("wave count").ToString() + " waves!";
    }
}
