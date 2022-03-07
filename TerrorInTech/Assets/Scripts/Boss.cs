using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private SpriteRenderer sr;
    public float speed = 0.03f;
    public float shotForce = 1;
    public Transform Player;
    public int health;
    private int total_health;

    private float shootCooldown;
    private float shootCooldownTotal;

    public GameObject coin;
    public GameObject fireBall;
    public Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();

        total_health = (PlayerPrefs.GetInt("wave count")/5 + 1) * 12;
        health = total_health;
        
        //shoot cooldown is the inverse of the # boss fight it is times 2.5 (2.5 is ez first fight to learn mechanic)
        shootCooldownTotal = 2.5f * 5f / (float)PlayerPrefs.GetInt("wave count");
        shootCooldown = shootCooldownTotal;

        Physics2D.IgnoreLayerCollision(14, 15);
    }

    void FixedUpdate()
    {
        if (shootCooldown <= 0)
        {
            GameObject fb = Instantiate(fireBall, firePoint.position, firePoint.rotation);
            Rigidbody2D body = fb.GetComponent<Rigidbody2D>();
            body.AddForce(firePoint.right * shotForce, ForceMode2D.Impulse);
            shootCooldown = shootCooldownTotal;
        }
        else
        {
            shootCooldown -= Time.fixedDeltaTime;
        }
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
            transform.localScale = new Vector3(0.175f, -0.175f, 1);
        }
        else
        {
            transform.localScale = new Vector3(0.175f, 0.175f, 1);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var GameObj = GetComponent<CapsuleCollider2D>();
        /* change healthbar according to new player health
            float new_x_scale = 3.0f * ((float)health/(float)total_health);
            float x_moved = (3 - new_x_scale) / 2f;
            var tf = healthbar.transform;
            tf.localScale = new Vector2(new_x_scale, 0.25f);
            tf.localPosition = new Vector2(-1 * x_moved, 2.5f);
            */
        if (GameObj.name == collision.collider.name)
        {
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
            for (int i = 0; i < 10; i++)
            {
                Instantiate(coin, transform.position, Quaternion.identity, transform.parent);
            }
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
