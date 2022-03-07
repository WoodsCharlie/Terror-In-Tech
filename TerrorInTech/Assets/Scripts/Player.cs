using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private SpriteRenderer mySpriteRend;

    public int health;
    private int speed;
    private int currency;

    private bool invincible = false;
    private float invincible_timer;

    public Text currencyText;
    public Text speedText;
    public Slider healthBar;
    public Image healthBarFill;
    public GameObject entershop;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(0.525f, 0.525f, 1);
        transform.position = new Vector2(PlayerPrefs.GetFloat("player x"), PlayerPrefs.GetFloat("player y"));

        rigidBody = GetComponent<Rigidbody2D>();
        mySpriteRend = GetComponent<SpriteRenderer>();

        if (PlayerPrefs.GetInt("gameStarted") == 0)
        {
            PlayerPrefs.SetInt("total health", 25);
            PlayerPrefs.SetInt("health", 25);
            PlayerPrefs.SetInt("speed", 3);
            PlayerPrefs.SetInt("enemy damage", 1);
            PlayerPrefs.SetInt("ianHealth", 50);
            PlayerPrefs.SetInt("total ianHealth", 50);
        }

        speed = PlayerPrefs.GetInt("speed");
        currency = PlayerPrefs.GetInt("currency");
        health = PlayerPrefs.GetInt("health");

        currencyText.text = "currency: " + currency.ToString();

        PlayerPrefs.SetInt("gameStarted", 1);
    }

    void FixedUpdate()
    {
        currencyText.text = "bank: $" + currency.ToString();

        var horizAxis = Input.GetAxis("Horizontal");
        var vertAxis = Input.GetAxis("Vertical");

        var vect = new Vector2(horizAxis, vertAxis);        

        rigidBody.velocity = (speed * vect);

        TurnToMouse();
        //rigidBody.angularVelocity = RotateSpeed * Input.GetAxis("Rotate");

        PlayerPrefs.SetFloat("player y", transform.position.y);
        PlayerPrefs.SetFloat("player x", transform.position.x);
        PlayerPrefs.SetInt("health", health);

        // update healthbar
        float healthpercent = (float)health / (float)PlayerPrefs.GetInt("total health");
        int healthvalue = (int)(healthpercent * 100);
        healthBar.value = healthvalue;

        // player selects which ammo they're using
        if (Input.GetKey(KeyCode.Alpha1))
            PlayerPrefs.SetInt("ammo_selection", 0);
        if (Input.GetKey(KeyCode.Alpha2))
            PlayerPrefs.SetInt("ammo_selection", 1);
        if (Input.GetKey(KeyCode.Alpha3))
            PlayerPrefs.SetInt("ammo_selection", 2);
        if (Input.GetKey(KeyCode.Alpha4))
            PlayerPrefs.SetInt("ammo_selection", 3);
        if (Input.GetKey(KeyCode.Alpha5))
            PlayerPrefs.SetInt("ammo_selection", 4);

        // if between waves player can access shop
        if (PlayerPrefs.GetInt("wave happening") == 1)
        {
            entershop.SetActive(true);
            if (Input.GetKey(KeyCode.F))
            {
                SceneManager.LoadScene("ShopScene");
            }
        }
        else{
            entershop.SetActive(false);
        }

        //exit game on pressing escape
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        //reset to wave 1
        if (Input.GetKey(KeyCode.Alpha0))
        {
            PlayerPrefs.SetInt("wave count", 0);
        }

        //testing skip to boss fight
        if (Input.GetKey(KeyCode.Alpha8))
        {
            PlayerPrefs.SetInt("wave count", 4);
        }

        //testing skip to wave 11
        if (Input.GetKey(KeyCode.Alpha9))
        {
            PlayerPrefs.SetInt("wave count", 10);
        }

        // making player invincible for half a second if they are hit by an enemy
        if (invincible)
        {
            if (invincible_timer >= 0){
                invincible_timer -= Time.fixedDeltaTime;
            }
            else{
                invincible = false;
            }
        }
        else{
            invincible_timer = 0.5f;
        }
        //making the game end if the player dies
        if (health <= 0)
        {
            SceneManager.LoadScene("DeathScene");
        }
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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            health -= PlayerPrefs.GetInt("enemy damage");
            //invincible = true;
            StartCoroutine(flashDamage());
        }
        if (collision.gameObject.name == "FireBall(Clone)")
        {
            health -= 4;
            StartCoroutine(flashDamage());
        }

        if (collision.gameObject.name == "Coin(Clone)")
        {
            currency += 1;
            PlayerPrefs.SetInt("currency", currency);
            Destroy(collision.gameObject);
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
