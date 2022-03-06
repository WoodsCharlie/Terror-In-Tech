using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
	public Transform Player;
	public Transform Ian;

	private int WaveCount = 0;
	public float timeBetweenWaves = 10f;
	private float waveCountdown;
	public Text WaveNumberText;
	public Text WaveCountdownText;

	public GameObject waveOverText;
	public Text wot;
	public GameObject newSpawnpointsText;

	public GameObject regDuck;
	public GameObject ianDuck;
	public GameObject ghostDuck;
	public GameObject boss;

	public GameObject tempDoor1;
	public GameObject tempDoor2;

	private int numBossFights = 0;
	private float spawnRate = 1;

	public enum SpawnState { SPAWNING, WAITING, COUNTING };

	public Transform[] spawnPoints;
	public Transform[] spawnPoints2;
	public Transform bossSp;

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
		if (WaveCount >= 10)
		{
			tempDoor1.SetActive(false);
			tempDoor2.SetActive(false);
		}
		if (spawnPoints.Length == 0)
		{
			Debug.LogError("No spawn points referenced.");
		}
		WaveNumberText.text = "wave number: " + WaveCount.ToString();
		waveCountdown = timeBetweenWaves;
	}

	void Update()
	{
		WaveCount = PlayerPrefs.GetInt("wave count");
		if (WaveCount == 10)
		{
			tempDoor1.SetActive(false);
			tempDoor2.SetActive(false);
		}

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
				WaveCountdownText.text = "time until \nnext wave: 0";
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
		float waitTime = (2.5f);
		if (WaveCount == 9)
        {
			newSpawnpointsText.SetActive(true);
			waitTime = 7.5f;
        }
		wot.text = "WAVE " + WaveCount.ToString() + " OVER!";
		waveOverText.SetActive(true);
		yield return new WaitForSeconds(waitTime);
		waveOverText.SetActive(false);
		newSpawnpointsText.SetActive(false);
	}

	IEnumerator SpawnWave()
	{
		state = SpawnState.SPAWNING;
		WaveCount += 1;
		PlayerPrefs.SetInt("wave count", WaveCount);
		WaveNumberText.text = "wave number: " + WaveCount.ToString();

		// first four waves just regular ducks
		if (WaveCount <= 4)
        {
			for (int i = 0; i < 3 * WaveCount; i++)
            {
				SpawnRegDuck(spawnPoints);
				yield return new WaitForSeconds(1f / spawnRate);
			}
		}
		else if (WaveCount % 5 == 0)
		{
			numBossFights += 1;
			BossFight();
		}
		else if (WaveCount >= 6 && WaveCount <= 9)
        {
			spawnRate = 1.5f;
			for (int i = 0; i < WaveCount - numBossFights; i++)
			{
				SpawnRegDuck(spawnPoints);
				yield return new WaitForSeconds(1f / spawnRate);
				SpawnRegDuck(spawnPoints);
				yield return new WaitForSeconds(1f / spawnRate);
				SpawnIanDuck(spawnPoints);
				yield return new WaitForSeconds(1f / spawnRate);
			}
		}
		else if (WaveCount >= 10)
		{
			spawnRate = 2;
			for (int i = 0; i < WaveCount - numBossFights; i++)
			{
				SpawnRegDuck(spawnPoints2);
				yield return new WaitForSeconds(1f / spawnRate);
				SpawnGhostDuck(spawnPoints2);
				yield return new WaitForSeconds(1f / spawnRate);
				SpawnIanDuck(spawnPoints);
				yield return new WaitForSeconds(1f / spawnRate);
			}
		}

		state = SpawnState.WAITING;
		yield break;
	}

	void BossFight()
    {
		GameObject ene = Instantiate(boss, bossSp.position, bossSp.rotation);
		Boss enemy_spawn = ene.GetComponent<Boss>();
		enemy_spawn.Player = Player;
	}

	void SpawnRegDuck(Transform[] sp)
	{
		Transform _sp = sp[Random.Range(0, sp.Length)];
		GameObject ene = Instantiate(regDuck, _sp.position, _sp.rotation);
		enemy enemy_spawn = ene.GetComponent<enemy>();
		enemy_spawn.Player = Player;
	}

	void SpawnIanDuck(Transform[] sp)
	{
		Transform _sp = sp[Random.Range(0, sp.Length)];
		GameObject ene = Instantiate(ianDuck, _sp.position, _sp.rotation);
		IanEnemy enemy_spawn = ene.GetComponent<IanEnemy>();
		enemy_spawn.Player = Ian;
	}

	void SpawnGhostDuck(Transform[] sp)
	{
		Transform _sp = sp[Random.Range(0, sp.Length)];
		GameObject ene = Instantiate(ghostDuck, _sp.position, _sp.rotation);
		AngelEnemy enemy_spawn = ene.GetComponent<AngelEnemy>();
		enemy_spawn.Player = Player;
	}
}
