using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float speed = 0.03f;
    public Transform Player;
    private int health;
    private int total_health;

    public GameObject coin;
    public GameObject fire1;
    public GameObject fire2;
    public GameObject fire3;

    public enum SpawnState { FIRING, WAITING };
    private SpawnState state = SpawnState.WAITING;

    // Start is called before the first frame update
    void Start()
    {
        total_health = (PlayerPrefs.GetInt("wave count")/5 + 1) * 12;
        health = total_health;
    }

    void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, Player.transform.position) < 5 && state == SpawnState.WAITING)
        {
            StartCoroutine(fire());
        }
        transform.position = Vector3.MoveTowards(transform.position, Player.position, speed);
        TurnToPlayer();
    }

    void TurnToPlayer()
    {
        Vector3 player_pos = Player.position;
        player_pos.z = 0f;
        player_pos.x = player_pos.x - transform.position.x;
        player_pos.y = player_pos.y - transform.position.y;

        float angle = Mathf.Atan2(player_pos.y, player_pos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (player_pos.x < 0)
        {
            transform.localScale = new Vector3(0.175f, -0.175f, 1);
        }
        else
        {
            transform.localScale = new Vector3(0.175f, 0.175f, 1);
        }
    }

    IEnumerator fire()
    {
        state = SpawnState.FIRING;
        fire1.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        fire1.SetActive(false);
        fire2.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        fire2.SetActive(false);
        fire3.SetActive(true);
        yield return new WaitForSeconds(1);
        fire3.SetActive(false);
        fire2.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        fire2.SetActive(false);
        fire1.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        fire1.SetActive(false);
        state = SpawnState.WAITING;
    }
}
