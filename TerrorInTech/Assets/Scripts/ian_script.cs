using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ian_script : MonoBehaviour
{
    public SpriteRenderer sr;
    public Sprite stage1;
    public Sprite stage2;
    public Sprite stage3;

    // Start is called before the first frame update
    void Start()
    {
        sr = GameObject.FindObjectOfType<SpriteRenderer>();
        sr.sprite = stage1;
        if (PlayerPrefs.GetInt("gameStarted") == 0)
        {
            PlayerPrefs.SetInt("ianHealth", 50);
        }
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("ianHealth") < 33)
            sr.sprite = stage2;
        if (PlayerPrefs.GetInt("ianHealth") < 17)
            sr.sprite = stage3;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Enemy")
            PlayerPrefs.SetInt("ianHealth", PlayerPrefs.GetInt("ianHealth") - PlayerPrefs.GetInt("enemy damage"));
    }
}
