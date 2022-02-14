using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform firePoint;
    public GameObject shotPrefab;

    public float shotForce = 5f; //aka speed (displacement?)
    public float shotDamage = 5f; 

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject bullet = Instantiate(shotPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D body = bullet.GetComponent<Rigidbody2D>();
            body.AddForce(firePoint.up * shotForce, ForceMode2D.Impulse);
        }   
    }
}
