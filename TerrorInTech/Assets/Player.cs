using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    private int speed;
    private int power;
    private int currency;

    public Text currencyText;
    public Text speedText;
    public Text powerText;

    public Button shopButton;

    // Start is called before the first frame update
    void Start()
    {
        shopButton.gameObject.SetActive(false);

        rigidBody = GetComponent<Rigidbody2D>();

        if (PlayerPrefs.GetInt("gameStarted") == 0)
        {
            PlayerPrefs.SetInt("speed", 3);
            PlayerPrefs.SetInt("power", 1);
            PlayerPrefs.SetInt("currency", 100);
        }

        speed = PlayerPrefs.GetInt("speed");
        power = PlayerPrefs.GetInt("power");
        currency = PlayerPrefs.GetInt("currency");

        speedText.text = "speed: " + speed.ToString();
        powerText.text = "power: " + power.ToString();
        currencyText.text = "currency: " + currency.ToString();

        PlayerPrefs.SetInt("gameStarted", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
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


    }

    void TurnToMouse()
    {
        Vector3 mouse_pos = Input.mousePosition;
        mouse_pos.z = 10;
        Vector3 object_pos = Camera.main.WorldToScreenPoint(transform.position);
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;
        float angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Store")
            shopButton.gameObject.SetActive(true);  
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Store")
            shopButton.gameObject.SetActive(false);
    }
}
