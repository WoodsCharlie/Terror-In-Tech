using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float speed = 0.03f;
    public Transform Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TurnToPlayer();
        transform.position = Vector3.MoveTowards(transform.position, Player.position, speed);
    }

    void TurnToPlayer()
    {
        Vector3 player_pos = Player.position;
        player_pos.z = 0f;
        player_pos.x = player_pos.x - transform.position.x;
        player_pos.y = player_pos.y - transform.position.y;

        float angle = Mathf.Atan2(player_pos.y, player_pos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var GameObj = GetComponent<BoxCollider2D>();
        if (GameObj.name == collision.collider.name){
            return;
        }
        if (collision.collider.name == "Player" || collision.collider.name == "Bullet(Clone)")
        {
            Destroy(gameObject);
        }
    }
}
