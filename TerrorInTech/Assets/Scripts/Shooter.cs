using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform firePoint;
    public GameObject redShotPrefab;
    public GameObject orangeShotPrefab;
    public GameObject greenShotPrefab;
    public GameObject yellowShotPrefab;
    public GameObject blueShotPrefab;
    public GameObject purpleShotPrefab;
    public Transform Player;

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
                GameObject bullet = Instantiate(redShotPrefab, firePoint.position, firePoint.rotation);
                Rigidbody2D body = bullet.GetComponent<Rigidbody2D>();
                body.AddForce(firePoint.up * shotForce, ForceMode2D.Impulse);
            }
                
            if (PlayerPrefs.GetInt("ammo_selection") == 1 & PlayerPrefs.GetInt("orange") > 0)
            {
                PlayerPrefs.SetInt("orange", PlayerPrefs.GetInt("orange") - 1);
                GameObject bullet = Instantiate(orangeShotPrefab, firePoint.position, firePoint.rotation);
                Rigidbody2D body = bullet.GetComponent<Rigidbody2D>();
                body.AddForce(firePoint.up * shotForce, ForceMode2D.Impulse);
            }
                
            if (PlayerPrefs.GetInt("ammo_selection") == 2 & PlayerPrefs.GetInt("yellow") > 0)
            {
                PlayerPrefs.SetInt("yellow", PlayerPrefs.GetInt("yellow") - 1);
                for (int i = 0; i < 3; i++)
                {
                    Vector3 temp = firePoint.up;
                    float rotation = Player.rotation.z;
                    if ((rotation >= 0.45 && rotation <= 0.90)){
                        temp.y += (i - 1) * 0.5f;
                    }
                    else if (rotation <= -0.45 && rotation >= -0.90)
                    {
                        temp.y += (i - 1) * 0.5f;
                    }
                    else
                    {
                        temp.x += (i - 1) * 0.5f;
                    }
                    GameObject bullet = Instantiate(greenShotPrefab, firePoint.position, firePoint.rotation);
                    Rigidbody2D body = bullet.GetComponent<Rigidbody2D>();
                    body.AddForce(temp * (float)shotForce/3, ForceMode2D.Impulse);
                }
            }

            if (PlayerPrefs.GetInt("ammo_selection") == 3 & PlayerPrefs.GetInt("green") > 0)
            {
                PlayerPrefs.SetInt("green", PlayerPrefs.GetInt("green") - 1);
                GameObject bullet = Instantiate(yellowShotPrefab, firePoint.position, firePoint.rotation);
                Rigidbody2D body = bullet.GetComponent<Rigidbody2D>();
                body.AddForce(firePoint.up * shotForce, ForceMode2D.Impulse);
            }
                
            if (PlayerPrefs.GetInt("ammo_selection") == 4 & PlayerPrefs.GetInt("blue") > 0)
            {
                PlayerPrefs.SetInt("blue", PlayerPrefs.GetInt("blue") - 1);
                GameObject bullet = Instantiate(blueShotPrefab, firePoint.position, firePoint.rotation);
                Rigidbody2D body = bullet.GetComponent<Rigidbody2D>();
                body.AddForce(firePoint.up * shotForce, ForceMode2D.Impulse);
            }   
            
            if (PlayerPrefs.GetInt("ammo_selection") == 5 & PlayerPrefs.GetInt("purple") > 0)
            {
                PlayerPrefs.SetInt("purple", PlayerPrefs.GetInt("purple") - 1);
                GameObject bullet = Instantiate(purpleShotPrefab, firePoint.position, firePoint.rotation);
                Rigidbody2D body = bullet.GetComponent<Rigidbody2D>();
                body.AddForce(firePoint.up * shotForce, ForceMode2D.Impulse);
            }

        }   
    }
}
