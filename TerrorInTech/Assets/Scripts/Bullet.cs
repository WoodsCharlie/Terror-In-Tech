using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Sprite red;
    public Sprite orange;
    public Sprite yellow;
    public Sprite green;
    public Sprite blue;
    public Sprite purple;

    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        Physics2D.IgnoreLayerCollision(10, 7);
    }

    private void FixedUpdate()
    {
    }

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
