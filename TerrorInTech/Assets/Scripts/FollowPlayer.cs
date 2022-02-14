using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    private Vector3 camera_offset;

    // Start is called before the first frame update
    void Start()
    {
        var zeros = new Vector3(0, 0, 0);
        camera_offset = transform.position - zeros;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + camera_offset;
    }
}
