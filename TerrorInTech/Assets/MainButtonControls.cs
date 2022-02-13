using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainButtonControls : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void enterShop()
    {
        SceneManager.LoadScene("ShopScene");
    }

    public void resetStats()
    {
        PlayerPrefs.DeleteAll();
    }
}
