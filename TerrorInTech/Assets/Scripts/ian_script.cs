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

    public Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {
        sr.sprite = stage1;
    }

    private void FixedUpdate()
    {
        if (PlayerPrefs.GetInt("ianHealth") < 33)
            sr.sprite = stage2;
        if (PlayerPrefs.GetInt("ianHealth") < 17)
            sr.sprite = stage3;

        float healthpercent = (float)PlayerPrefs.GetInt("ianHealth") / (float)50;
        int healthvalue = (int)(healthpercent * 100);
        healthBar.value = healthvalue;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Enemy")
            PlayerPrefs.SetInt("ianHealth", PlayerPrefs.GetInt("ianHealth") - PlayerPrefs.GetInt("enemy damage"));
    }
}
