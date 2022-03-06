using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IanEnemy : MonoBehaviour
{
    private SpriteRenderer sr;
    public float speed = 0.03f;
    public Transform Player;
    public GameObject healthbar;
    private int health;
    private int total_health;
    public GameObject coin;
    private float knockbackDurration = 1f;
    private float currentKnockbackTimer = 1f;
    private float original_speed;
    private float orginal_mass;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
        rb = this.GetComponent<Rigidbody2D>();
        total_health = (PlayerPrefs.GetInt("wave count") - 1) / 5 + 1;
        health = total_health;
        Physics2D.IgnoreLayerCollision(6, 7);
        original_speed = speed;
        orginal_mass = rb.mass;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentKnockbackTimer >= knockbackDurration){
            transform.position = Vector3.MoveTowards(transform.position, Player.position, speed);
        }
        if (currentKnockbackTimer < knockbackDurration){
            transform.position = Vector3.MoveTowards(transform.position, Player.position, -speed);
            currentKnockbackTimer = currentKnockbackTimer + 0.05f;
        }
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
        var GameObj = GetComponent<BoxCollider2D>();
        if (GameObj.name == collision.collider.name)
        {
            return;
        }

        if (collision.collider.name == "Ian")
        {
            currentKnockbackTimer = 0f;
        }

        if (collision.collider.name == "BulletRed(Clone)" || collision.collider.name == "BulletYellow(Clone)" || collision.collider.name == "BulletGreen(Clone)" || collision.collider.name == "BulletOrange(Clone)" || collision.collider.name == "BulletIndigo(Clone)")
        {
            StartCoroutine(flashDamage());
        }

        if (collision.collider.name == "BulletRed(Clone)" || collision.collider.name == "BulletYellow(Clone)" || collision.collider.name == "BulletIndigo(Clone)")
        {
            health -= 1;
        }
        if (collision.collider.name == "BulletBlue(Clone)")
        {
            StartCoroutine(freezeEnemy());
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
    IEnumerator freezeEnemy()
    {
        sr.color = Color.blue;
        speed = 0;
        rb.mass *= 10;
        yield return new WaitForSeconds(1f);
        sr.color = Color.white;
        speed = original_speed;
        rb.mass = orginal_mass;
    }
}

