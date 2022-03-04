using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class enemy : MonoBehaviour
{
    private SpriteRenderer sr;
    public float speed = 0.03f;
    public Transform Player;
    public GameObject healthbar;
    private int health;
    private int total_health;
    public GameObject coin;

    // Start is called before the first frame update
    void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
        total_health = (PlayerPrefs.GetInt("wave count") - 1) / 5 + 1;
        health = total_health;
        Physics2D.IgnoreLayerCollision(11, 7);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, Player.position, speed);
        TurnToPlayer();
    }

    void TurnToPlayer()
    {
        Vector3 player_pos = Player.position;
        player_pos.z = 0f;
        player_pos.x = player_pos.x - transform.position.x;
        player_pos.y = player_pos.y - transform.position.y;

        float angle = Mathf.Atan2(player_pos.y, player_pos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (player_pos.x < 0)
            {
            transform.localScale = new Vector3(0.175f,-0.175f,1);
        }
        else{
            transform.localScale = new Vector3(0.175f,0.175f,1);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var GameObj = GetComponent<BoxCollider2D>();
        /* change healthbar according to new player health
            float new_x_scale = 3.0f * ((float)health/(float)total_health);
            float x_moved = (3 - new_x_scale) / 2f;
            var tf = healthbar.transform;
            tf.localScale = new Vector2(new_x_scale, 0.25f);
            tf.localPosition = new Vector2(-1 * x_moved, 2.5f);
            */
        if (GameObj.name == collision.collider.name) {
            return;
        }

        if (collision.collider.name == "BulletRed(Clone)" || collision.collider.name == "BulletYellow(Clone)" || collision.collider.name == "BulletGreen(Clone)" || collision.collider.name == "BulletOrange(Clone)" || collision.collider.name == "BulletPurple(Clone)")
        {
            StartCoroutine(flashDamage());
        }

        if (collision.collider.name == "BulletRed(Clone)" || collision.collider.name == "BulletYellow(Clone)")
        {
            health -= 1;
        }
        if (collision.collider.name == "BulletBlue(Clone)")
        {
            speed = 0;
        }
        if (collision.collider.name == "BulletGreen(Clone)")
        {
            health -= 1;

            if (speed > 0)
            {
                speed -= 0.01f;
            }
            else
            {
                speed = 0;
            }
        }
        if (collision.collider.name == "BulletOrange(Clone)")
        {
            health -= 2;
        }
        if (health <= 0)
        {
            Instantiate(coin, transform.position, Quaternion.identity, transform.parent);
            Destroy(gameObject);
        }
    }

    IEnumerator flashDamage()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        sr.color = Color.white;
    }
}
