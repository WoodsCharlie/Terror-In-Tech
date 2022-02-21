using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
	public Transform Player;
	public Text WaveNumberText;
	public Text WaveCountdownText;
	private int WaveCount = 0;
	public enum SpawnState { SPAWNING, WAITING, COUNTING };

	[System.Serializable]
	public class Wave
	{
		public string name;
		public GameObject enemy;
		public int count;
		public float rate;
	}

	public Wave[] waves;
	private int nextWave = 0;
	public int NextWave
	{
		get { return nextWave + 1; }
	}

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
				StartCoroutine(SpawnWave(waves[nextWave]));
			}
		}
		else
		{
			waveCountdown -= Time.deltaTime;
			if (waveCountdown >= 0)
			{
				WaveCountdownText.text = "time until next wave: " + ((int)waveCountdown).ToString();
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

		if (nextWave + 1 > waves.Length - 1)
		{
			for (int i = 0; i < waves.Length; i++)
			{
				waves[i].count += 5;
			}
			nextWave = 0;
		}
		else
		{
			nextWave++;
		}
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

	IEnumerator SpawnWave(Wave _wave)
	{
		state = SpawnState.SPAWNING;
		WaveCount += 1;
		PlayerPrefs.SetInt("wave count", WaveCount);
		WaveNumberText.text = "wave number: " + WaveCount.ToString();

		for (int i = 0; i < _wave.count; i++)
		{
			SpawnEnemy(_wave.enemy);
			yield return new WaitForSeconds(1f / _wave.rate);
		}

		state = SpawnState.WAITING;

		yield break;
	}

	void SpawnEnemy(GameObject _enemy)
	{
		Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
		GameObject ene = Instantiate(_enemy, _sp.position, _sp.rotation);
		enemy enemy_spawn = ene.GetComponent<enemy>();
		enemy_spawn.Player = Player;
	}

}
