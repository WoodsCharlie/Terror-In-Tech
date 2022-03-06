using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialization : MonoBehaviour
{
    // Start is called before the first frame update
    public float followSpeed = 0.001f;
    public Texture2D cursorTexture;
    void Start()
    {
        Screen.SetResolution(1280, 720, true);
        Cursor.SetCursor(cursorTexture, new Vector2(0,0), CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
       //transform.position = Vector3.Lerp(transform.position, Player.position + new Vector3(0, 0, -10), followSpeed);
    }
}
