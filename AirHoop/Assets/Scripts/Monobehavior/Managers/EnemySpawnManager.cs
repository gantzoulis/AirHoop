using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour 
{
	public GameObject[] enemy;
	private int randEnemy;
	private GameObject spawnEnemy;

	private float maxDist;
	private float curDist;
	private float enemySize = 0.0f;

	private float randX;
	private const float distX = 60.0f; 
	private float randY;
	private const float distY = 6.0f;

	private Vector3 whereToSpawn;

	public float spawnRate = 2.5f;
	private float nextSpawn = 0.0f;

	void Start()
	{
		maxDist = GameManager.Instance.playerObject.transform.position.x;
	}

	void FixedUpdate()
	{
		SpawnEngine();
	}

	private void SpawnEngine()
	{
		curDist = GameManager.Instance.playerObject.transform.position.x;
			
		if(Time.time > nextSpawn && curDist > maxDist + enemySize)
		{
			randEnemy = Random.Range(0, enemy.Length);
			spawnEnemy = enemy[randEnemy];

			nextSpawn = Time.time + spawnRate;

			randX = GameManager.Instance.playerObject.transform.position.x + distX;
			randY = Random.Range(GameManager.Instance.minAirplaneHeight + distY, GameManager.Instance.maxAirplaneHeight);
			whereToSpawn = new Vector3(randX, randY, 0);

			Instantiate(spawnEnemy, whereToSpawn, Quaternion.identity);
		}

		if(curDist >= maxDist)
		{
			maxDist = curDist;
		}
	}

}
