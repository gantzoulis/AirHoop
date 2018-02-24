using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour 
{
	public GameObject[] enemy;
	private int randEnemy;
	private GameObject spawnEnemy;

	private float randX;
	private const float distX = 60.0f; 
	private float randY;
	private const float distY = 6.0f;

	private Vector3 whereToSpawn;

	public float spawnRate = 2.5f;
	private float nextSpawn = 0.0f;

	void Update()
	{
		SpawnEngine();
	}

	private void SpawnEngine()
	{
		if(Time.time > nextSpawn)
		{
			randEnemy = Random.Range(0, enemy.Length);
			spawnEnemy = enemy[randEnemy];

			nextSpawn = Time.time + spawnRate;

			randX = GameManager.Instance.playerObject.transform.position.x + distX;
			randY = Random.Range(GameManager.Instance.minAirplaneHeight + distY, GameManager.Instance.maxAirplaneHeight);
			whereToSpawn = new Vector3(randX, randY, 0);

			Instantiate(spawnEnemy, whereToSpawn, Quaternion.identity);
		}
	}

}
