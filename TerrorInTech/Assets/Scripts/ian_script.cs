using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ian_script : MonoBehaviour
{
    public SpriteRenderer sr;
    public Sprite stage1;
    public Sprite stage2;
    public Sprite stage3;
    private bool invincible = false;
    private float invincible_timer;

    public Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("total ianHealth", 50);
        sr.sprite = stage1;
    }

    private void FixedUpdate()
    {
        if (PlayerPrefs.GetInt("ianHealth") < 33)
            sr.sprite = stage2;
        if (PlayerPrefs.GetInt("ianHealth") < 17)
            sr.sprite = stage3;

        float healthpercent = (float)PlayerPrefs.GetInt("ianHealth") / (float)PlayerPrefs.GetInt("total ianHealth");
        int healthvalue = (int)(healthpercent * 100);
        healthBar.value = healthvalue;

        if (Input.GetKey(KeyCode.G))
            PlayerPrefs.SetInt("ianHealth", 32);
        if (Input.GetKey(KeyCode.H))
            PlayerPrefs.SetInt("ianHealth", 16);

        // making ian invincible for half a second if they are hit by an enemy
        if (invincible)
        {
            if (invincible_timer > 0)
                invincible_timer -= Time.fixedDeltaTime;
            else
                invincible = false;
        }
        else
            invincible_timer = 0.5f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Duck(Clone)" || collision.gameObject.name == "GhostDuck(Clone)" || collision.gameObject.name == "IanDuck(Clone)")
        {
            if (!invincible)
            {
                PlayerPrefs.SetInt("ianHealth", PlayerPrefs.GetInt("ianHealth") - PlayerPrefs.GetInt("enemy damage"));
                invincible = true;
            }
        }     
    }
}
