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
        if (PlayerPrefs.GetInt("ammo_selection") == 0)
            sr.sprite = red;

        if (PlayerPrefs.GetInt("ammo_selection") == 1)
            sr.sprite = orange;

        if (PlayerPrefs.GetInt("ammo_selection") == 2)
            sr.sprite = yellow;

        if (PlayerPrefs.GetInt("ammo_selection") == 3)
            sr.sprite = green;

        if (PlayerPrefs.GetInt("ammo_selection") == 4)
            sr.sprite = blue;

        if (PlayerPrefs.GetInt("ammo_selection") == 5)
            sr.sprite = purple;
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
