using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
	public Transform Player;
	public Transform Ian;
	public Text WaveNumberText;
	public Text WaveCountdownText;
	public GameObject regDuck;
	public GameObject ianDuck;
	public GameObject ghostDuck;
	public GameObject waveOverText;
	private float spawnRate = 1;
	private int WaveCount = 0;
	public enum SpawnState { SPAWNING, WAITING, COUNTING };

	public Transform[] spawnPoints;

	public float timeBetweenWaves = 10f;
	private float waveCountdown;
	public float WaveCountdown
	{
		get { return waveCountdown; }
	}

	private float searchCountdown = 1f;

	private SpawnState state = SpawnState.COUNTING;
	public SpawnState State
	{
		get { return state; }
	}

	void Start()
	{
		WaveCount = PlayerPrefs.GetInt("wave count");
		if (spawnPoints.Length == 0)
		{
			Debug.LogError("No spawn points referenced.");
		}

		waveCountdown = timeBetweenWaves;
	}

	void Update()
	{
		if (state == SpawnState.WAITING)
		{
			if (!EnemyIsAlive())
			{
				WaveCompleted();
			}
			else
			{
				return;
			}
		}

		if (waveCountdown <= 0)
		{
			if (state != SpawnState.SPAWNING)
			{
				StartCoroutine(SpawnWave());
			}
		}
		else
		{
			waveCountdown -= Time.deltaTime;
			if (waveCountdown >= 0)
			{
				WaveCountdownText.text = "time until \nnext wave: " + ((int)waveCountdown).ToString();
				PlayerPrefs.SetInt("wave happening", 1);
			}
			else
			{
				WaveCountdownText.text = "time until next wave: 0";
				PlayerPrefs.SetInt("wave happening", 0);
			}
		}
	}

	void WaveCompleted()
	{
		state = SpawnState.COUNTING;
		waveCountdown = timeBetweenWaves;
		StartCoroutine(showText());
	}

	bool EnemyIsAlive()
	{
		searchCountdown -= Time.deltaTime;
		if (searchCountdown <= 0f)
		{
			searchCountdown = 1f;
			if (GameObject.FindGameObjectWithTag("Enemy") == null)
			{
				return false;
			}
		}
		return true;
	}

	IEnumerator showText()
    {
		waveOverText.SetActive(true);
		yield return new WaitForSeconds(2.5f);
		waveOverText.SetActive(false);
	}

	IEnumerator SpawnWave()
	{
		state = SpawnState.SPAWNING;
		WaveCount += 1;
		PlayerPrefs.SetInt("wave count", WaveCount);
		WaveNumberText.text = "wave number: " + WaveCount.ToString();

		if (WaveCount <= 2)
        {
			for (int i = 0; i < 4*WaveCount; i++)
            {
				SpawnRegDuck();
				yield return new WaitForSeconds(1f / spawnRate);
			}
		}

		if (WaveCount <= 4 && WaveCount >= 3)
		{
			spawnRate = 1.5f;
			for (int i = 0; i < 2 * (WaveCount-1); i++)
			{
				SpawnRegDuck();
				SpawnIanDuck();
				SpawnRegDuck();
				yield return new WaitForSeconds(1f / spawnRate);
			}
		}

		if (WaveCount >= 5)
		{
			spawnRate = 2.0f;
			for (int i = 0; i < WaveCount; i++)
            {
				SpawnRegDuck();
				SpawnIanDuck();
				SpawnGhostDuck();
				SpawnRegDuck();
				yield return new WaitForSeconds(1f / spawnRate);
			}				
		}

		state = SpawnState.WAITING;
		yield break;
	}

	void SpawnRegDuck()
	{
		Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
		GameObject ene = Instantiate(regDuck, _sp.position, _sp.rotation);
		enemy enemy_spawn = ene.GetComponent<enemy>();
		enemy_spawn.Player = Player;
	}

	void SpawnIanDuck()
	{
		Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
		GameObject ene = Instantiate(ianDuck, _sp.position, _sp.rotation);
		IanEnemy enemy_spawn = ene.GetComponent<IanEnemy>();
		enemy_spawn.Player = Ian;
	}

	void SpawnGhostDuck()
	{
		Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
		GameObject ene = Instantiate(ghostDuck, _sp.position, _sp.rotation);
		AngelEnemy enemy_spawn = ene.GetComponent<AngelEnemy>();
		enemy_spawn.Player = Player;
	}
}
