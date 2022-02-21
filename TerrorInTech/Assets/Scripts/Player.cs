using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private SpriteRenderer mySpriteRend;

    private int health;
    private int speed;
    private int currency;

    public Text currencyText;
    public Text speedText;
    public Slider healthBar;

    public Button shopButton;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector2(PlayerPrefs.GetFloat("player x"), PlayerPrefs.GetFloat("player y"));

        shopButton.gameObject.SetActive(false);

        rigidBody = GetComponent<Rigidbody2D>();
        mySpriteRend = GetComponent<SpriteRenderer>();

        if (PlayerPrefs.GetInt("gameStarted") == 0)
        {
            PlayerPrefs.SetInt("total health", 10);
            PlayerPrefs.SetInt("health", 10);
            PlayerPrefs.SetInt("total health", 100);
            PlayerPrefs.SetInt("health", 100);
            PlayerPrefs.SetInt("speed", 3);
            PlayerPrefs.SetInt("currency", 100);
            PlayerPrefs.SetInt("enemy damage", 10);
        }

        speed = PlayerPrefs.GetInt("speed");
        currency = PlayerPrefs.GetInt("currency");
        health = 100;

        speedText.text = "speed: " + speed.ToString();
        currencyText.text = "currency: " + currency.ToString();

        PlayerPrefs.SetInt("gameStarted", 1);
    }

    void FixedUpdate()
    {
        currencyText.text = "currency: " + currency.ToString();

        var horizAxis = Input.GetAxis("Horizontal");
        var vertAxis = Input.GetAxis("Vertical");

        var vect = new Vector2(horizAxis, vertAxis);        

        rigidBody.velocity = (speed * vect);

        TurnToMouse();
        //rigidBody.angularVelocity = RotateSpeed * Input.GetAxis("Rotate");

        PlayerPrefs.SetFloat("player y", transform.position.y);
        PlayerPrefs.SetFloat("player x", transform.position.x);
        PlayerPrefs.SetInt("health", health);
        float healthpercent = (float)health / (float)PlayerPrefs.GetInt("total health");
        int healthvalue = (int)(healthpercent * 100);
        healthBar.value = healthvalue;
    }

    void TurnToMouse()
    {
        Vector3 mouse_pos = Input.mousePosition;
        mouse_pos.z = 10;
        Vector3 object_pos = Camera.main.WorldToScreenPoint(transform.position);
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;

        
        float angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (mouse_pos.x < 0){
            transform.localScale = new Vector3(-0.2f,0.2f,1);
        }
        else{
            transform.localScale = new Vector3(0.2f,0.2f,1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Store")
            shopButton.gameObject.SetActive(true);
        if (collision.gameObject.name == "Enemy(Clone)")
            health -= PlayerPrefs.GetInt("enemy damage");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Store")
            shopButton.gameObject.SetActive(false);
    }
}
