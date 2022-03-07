using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform firePoint;
    public GameObject redShotPrefab;
    public GameObject orangeShotPrefab;
    public GameObject yellowShotPrefab;
    public GameObject blueShotPrefab;
    public GameObject purpleShotPrefab;
    public Transform Player;

    public float shotForce = 1000f; //aka speed (displacement?)
    public float shotDamage = 5f; 
    public int shotsPerReload = 12;
    public int shotsLeft;
    public float reloadSpeed = 1f;
    public float shotCooldownTotal = 0.05f;
    public float shotCooldown = 0.05f;

    void Start()
    {
        shotsLeft = 12;
        shotCooldownTotal = 0.05f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        shotForce = Mathf.Log(PlayerPrefs.GetInt("speed")) * 5;
        if (Input.GetButton("Fire1") & (shotCooldown >= shotCooldownTotal))
        {
            if (PlayerPrefs.GetInt("ammo_selection") == 0)
            {
                GameObject bullet = Instantiate(redShotPrefab, firePoint.position, firePoint.rotation);
                Rigidbody2D body = bullet.GetComponent<Rigidbody2D>();
                body.AddForce(firePoint.up * shotForce, ForceMode2D.Impulse);
            }
                
            if (PlayerPrefs.GetInt("ammo_selection") == 1)
            {
                if (PlayerPrefs.GetInt("orange") > 0)
                {
                    PlayerPrefs.SetInt("orange", PlayerPrefs.GetInt("orange") - 1);
                    GameObject bullet = Instantiate(orangeShotPrefab, firePoint.position, firePoint.rotation);
                    Rigidbody2D body = bullet.GetComponent<Rigidbody2D>();
                    body.AddForce(firePoint.up * shotForce, ForceMode2D.Impulse);
                }
                else{
                    PlayerPrefs.SetInt("ammo_selection", 0);
                }
            }
                
            if (PlayerPrefs.GetInt("ammo_selection") == 2)
            {
                if (PlayerPrefs.GetInt("yellow") > 0)
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
                        GameObject bullet = Instantiate(yellowShotPrefab, firePoint.position, firePoint.rotation);
                        Rigidbody2D body = bullet.GetComponent<Rigidbody2D>();
                        body.AddForce(temp * (float)shotForce/3, ForceMode2D.Impulse);
                    }
                }
                else{
                    PlayerPrefs.SetInt("ammo_selection", 0);
                }
            }

               
            if (PlayerPrefs.GetInt("ammo_selection") == 3)
            {
                if (PlayerPrefs.GetInt("blue") > 0)
                {
                    PlayerPrefs.SetInt("blue", PlayerPrefs.GetInt("blue") - 1);
                    GameObject bullet = Instantiate(blueShotPrefab, firePoint.position, firePoint.rotation);
                    Rigidbody2D body = bullet.GetComponent<Rigidbody2D>();
                    body.AddForce(firePoint.up * shotForce, ForceMode2D.Impulse);
                }   
                else{
                    PlayerPrefs.SetInt("ammo_selection", 0);
                }
            }

            if (PlayerPrefs.GetInt("ammo_selection") == 4)
            {
                if (PlayerPrefs.GetInt("purple") > 0)
                {
                    PlayerPrefs.SetInt("purple", PlayerPrefs.GetInt("purple") - 1);
                    GameObject bullet = Instantiate(purpleShotPrefab, firePoint.position, firePoint.rotation);
                    Rigidbody2D body = bullet.GetComponent<Rigidbody2D>();
                    body.AddForce(firePoint.up * shotForce, ForceMode2D.Impulse);
                }
                else{
                    PlayerPrefs.SetInt("ammo_selection", 0);
                }
            }

        shotCooldown = 0f;
        shotsLeft = shotsLeft - 1;
        }

    if (Input.GetKeyDown("r") & (shotsLeft < shotsPerReload))
    {
        shotsLeft = shotsPerReload;
        shotCooldown = shotCooldown - 1f;
    }
    
    if (shotsLeft <= 0)
    {
        shotsLeft = shotsPerReload;
        shotCooldown = shotCooldown - 1.25f;
    }

    if (shotCooldown < shotCooldownTotal){
        shotCooldown += Time.deltaTime;
    } 

    }
}
