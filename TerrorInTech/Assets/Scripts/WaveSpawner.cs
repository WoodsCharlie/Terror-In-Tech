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
		if (spawnPoints.Length == 0)
		{
			Debug.LogError("No spawn points referenced.");
		}

		waveCountdown = timeBetweenWaves;
		PlayerPrefs.SetFloat("waveCountdown", waveCountdown);
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
				WaveCountdownText.text = "Time until next wave: " + ((int)waveCountdown).ToString();
			}
			else
			{
				WaveCountdownText.text = "Time until next wave: 0";
			}
			PlayerPrefs.SetFloat("waveCountdown", waveCountdown);
		}
	}

	void WaveCompleted()
	{

		state = SpawnState.COUNTING;
		PlayerPrefs.SetInt("wave happening", 0);
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
		PlayerPrefs.SetInt("wave happening", 1);
		WaveCount += 1;
		WaveNumberText.text = "Wave Number: " + WaveCount.ToString();

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
