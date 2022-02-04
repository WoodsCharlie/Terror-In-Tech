using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    public float EnginePower = 3;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        var horizAxis = Input.GetAxis("Horizontal");
        var vertAxis = Input.GetAxis("Vertical");

        var vect = new Vector2(horizAxis, vertAxis);        

        rigidBody.velocity = (EnginePower * vect);

        if (transform.position.x < -8.4){
            transform.position = new Vector3(-8.4f, transform.position.y , transform.position.z);
        }
        if (transform.position.x > 8.4){
            transform.position = new Vector3(8.4f, transform.position.y , transform.position.z);
        }
        if (transform.position.y < -4.45){
            transform.position = new Vector3(transform.position.x, -4.45f , transform.position.z);
        }
        if (transform.position.y > 4.45){
            transform.position = new Vector3(transform.position.x, 4.45f , transform.position.z);
        }

        //rigidBody.angularVelocity = RotateSpeed * Input.GetAxis("Rotate");

    }
}
