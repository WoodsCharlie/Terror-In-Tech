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
            if (PlayerPrefs.GetInt("ammo_selection") == 0)
            {
                GameObject bullet = Instantiate(shotPrefab, firePoint.position, firePoint.rotation);
                Rigidbody2D body = bullet.GetComponent<Rigidbody2D>();
                body.AddForce(firePoint.up * shotForce, ForceMode2D.Impulse);
            }
                
            if (PlayerPrefs.GetInt("ammo_selection") == 1 & PlayerPrefs.GetInt("orange") > 0)
            {
                PlayerPrefs.SetInt("orange", PlayerPrefs.GetInt("orange") - 1);
                GameObject bullet = Instantiate(shotPrefab, firePoint.position, firePoint.rotation);
                Rigidbody2D body = bullet.GetComponent<Rigidbody2D>();
                body.AddForce(firePoint.up * shotForce, ForceMode2D.Impulse);
            }
                
            if (PlayerPrefs.GetInt("ammo_selection") == 2 & PlayerPrefs.GetInt("yellow") > 0)
            {
                PlayerPrefs.SetInt("orange", PlayerPrefs.GetInt("yellow") - 1);
                GameObject bullet = Instantiate(shotPrefab, firePoint.position, firePoint.rotation);
                Rigidbody2D body = bullet.GetComponent<Rigidbody2D>();
                body.AddForce(firePoint.up * shotForce, ForceMode2D.Impulse);
            }

            if (PlayerPrefs.GetInt("ammo_selection") == 3 & PlayerPrefs.GetInt("green") > 0)
            {
                PlayerPrefs.SetInt("orange", PlayerPrefs.GetInt("green") - 1);
                GameObject bullet = Instantiate(shotPrefab, firePoint.position, firePoint.rotation);
                Rigidbody2D body = bullet.GetComponent<Rigidbody2D>();
                body.AddForce(firePoint.up * shotForce, ForceMode2D.Impulse);
            }
                
            if (PlayerPrefs.GetInt("ammo_selection") == 4 & PlayerPrefs.GetInt("blue") > 0)
            {
                PlayerPrefs.SetInt("orange", PlayerPrefs.GetInt("blue") - 1);
                GameObject bullet = Instantiate(shotPrefab, firePoint.position, firePoint.rotation);
                Rigidbody2D body = bullet.GetComponent<Rigidbody2D>();
                body.AddForce(firePoint.up * shotForce, ForceMode2D.Impulse);
            }   
            
            if (PlayerPrefs.GetInt("ammo_selection") == 5 & PlayerPrefs.GetInt("purple") > 0)
            {
                PlayerPrefs.SetInt("orange", PlayerPrefs.GetInt("purple") - 1);
                GameObject bullet = Instantiate(shotPrefab, firePoint.position, firePoint.rotation);
                Rigidbody2D body = bullet.GetComponent<Rigidbody2D>();
                body.AddForce(firePoint.up * shotForce, ForceMode2D.Impulse);
            }

        }   
    }
}
