using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ian_script : MonoBehaviour
{
    public SpriteRenderer sr;
    public Sprite stage1;
    public Sprite stage2;
    public Sprite stage3;
    private bool invincible = false;
    private float invincible_timer;

    public Slider healthBar;
    public Image healthBarFill;

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

        float healthpercent = (float)PlayerPrefs.GetInt("ianHealth") / (float)PlayerPrefs.GetInt("total ianHealth");
        int healthvalue = (int)(healthpercent * 100);
        healthBar.value = healthvalue;

        if (Input.GetKey(KeyCode.G))
            PlayerPrefs.SetInt("ianHealth", 32);
        if (Input.GetKey(KeyCode.H))
            PlayerPrefs.SetInt("ianHealth", 16);

        //game ends when Ian runs out of hp

        if (PlayerPrefs.GetInt("ianHealth") <= 0)
            SceneManager.LoadScene("DeathScene");


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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!invincible)
            {
                PlayerPrefs.SetInt("ianHealth", PlayerPrefs.GetInt("ianHealth") - PlayerPrefs.GetInt("enemy damage"));
                StartCoroutine(flashDamage());
                invincible = true;
            }
        }     
    }

    IEnumerator flashDamage()
    {
        for (int i = 0; i < 3; i++)
        {
            healthBarFill.color = Color.yellow;
            yield return new WaitForSeconds(0.1f);
            healthBarFill.color = Color.red;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
