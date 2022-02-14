using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    public float EnginePower = 3;
    public float health;
    public float currency;

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

        
        TurnToMouse();
        //rigidBody.angularVelocity = RotateSpeed * Input.GetAxis("Rotate");

    }

    void TurnToMouse()
    {
        Vector3 mouse_pos = Input.mousePosition;
        mouse_pos.z = 10;
        Vector3 object_pos = Camera.main.WorldToScreenPoint(transform.position);
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;
        float angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
