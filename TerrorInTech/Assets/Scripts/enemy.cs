using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float speed = 0.005f;
    public Transform Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, Player.position, speed);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var GameObj = GetComponent<BoxCollider2D>();
        if (GameObj.name == collision.collider.name){
            return;
        }
        if (collision.collider.name == "Player")
        {
            Destroy(gameObject);
        }
    }
}
