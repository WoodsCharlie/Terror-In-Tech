using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ammo_manager : MonoBehaviour
{
    public Text orange;
    public Text yellow;
    public Text green;
    public Text blue;
    public Text purple;

    public GameObject red_selected;
    public GameObject orange_selected;
    public GameObject yellow_selected;
    public GameObject green_selected;
    public GameObject blue_selected;
    public GameObject purple_selected;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        orange.text = PlayerPrefs.GetInt("orange").ToString();
        yellow.text = PlayerPrefs.GetInt("yellow").ToString();
        green.text = PlayerPrefs.GetInt("green").ToString();
        blue.text = PlayerPrefs.GetInt("blue").ToString();
        purple.text = PlayerPrefs.GetInt("purple").ToString();

        if (PlayerPrefs.GetInt("ammo_selection") == 0)
            red_selected.SetActive(true);
        else
            red_selected.SetActive(false);

        if (PlayerPrefs.GetInt("ammo_selection") == 1)
            orange_selected.SetActive(true);
        else
            orange_selected.SetActive(false);

        if (PlayerPrefs.GetInt("ammo_selection") == 2)
            yellow_selected.SetActive(true);
        else
            yellow_selected.SetActive(false);

        if (PlayerPrefs.GetInt("ammo_selection") == 3)
            green_selected.SetActive(true);
        else
            green_selected.SetActive(false);

        if (PlayerPrefs.GetInt("ammo_selection") == 4)
            blue_selected.SetActive(true);
        else
            blue_selected.SetActive(false);

        if (PlayerPrefs.GetInt("ammo_selection") == 5)
            purple_selected.SetActive(true);
        else
            purple_selected.SetActive(false);
    }
}
