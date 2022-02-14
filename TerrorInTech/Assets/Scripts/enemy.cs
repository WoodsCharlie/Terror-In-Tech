using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    private bool paused;
    private float pauseTime = 0;

    public float speed = 0.03f;
    public float health;
    public Transform Player;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        health = 1;
        paused = false;
        damage = 1;
        PlayerPrefs.SetInt("enemy damage", damage);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //enemy moves towards player
        if (!paused)
            transform.position = Vector3.MoveTowards(transform.position, Player.position, speed);
        else
        {
            pauseTime += Time.fixedDeltaTime;
            if (pauseTime > 1f)
            {
                paused = false;
                pauseTime = 0;
            }
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var GameObj = GetComponent<BoxCollider2D>();
        if (GameObj.name == collision.collider.name){
            return;
        }
        if (collision.collider.name == "Bullet(Clone)")
        {
            health -= 1;
        }
        if (collision.collider.name == "Player")
        {
            paused = true;
        }
    }
}
