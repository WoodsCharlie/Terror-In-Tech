using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        var GameObj = GetComponent<CapsuleCollider2D>();
        if (GameObj.name == collision.collider.name || "Player" == collision.collider.name){
            return;
        }
        Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
