using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemySpawnManager : MonoBehaviour 
{
	private const string ENEMY_PREFAB_PATH = "Prefabs/Enemies/GroundEnemies";
	private string enemyPrefabName;
	private string spawnString;

	private float randSpawnedObj;

	[SerializeField]
	private GameObject[] enemyList;
	private int randEnemy;
	private GameObject spawnEnemy;

	private float maxDist;
	private float curDist;
	private float overlapSize = 5.0f;

	public float spawnRate = 5f;
	private float nextSpawn = 0.0f;

	private float randX;
	private const float distX = 80.0f; 

	private Vector3 whereToSpawn;

	void Start()
	{
		enemyList = Resources.LoadAll<GameObject>(ENEMY_PREFAB_PATH);
		maxDist = GameManager.Instance.playerObject.transform.position.x;
	}

	void FixedUpdate()
	{
		SpawnEngine();
	}

	private void SpawnEngine()
	{
		if(GameManager.Instance.playerObject)
		{
			curDist = GameManager.Instance.playerObject.transform.position.x;
		}

		if(Time.time > nextSpawn && (curDist > maxDist + overlapSize))
		{
			randEnemy = Random.Range(0, enemyList.Length);
			spawnEnemy = enemyList[randEnemy];

			enemyPrefabName = spawnEnemy.name.ToString();
			spawnString = ENEMY_PREFAB_PATH + "/" + enemyPrefabName;

			nextSpawn = Time.time + spawnRate;

			if (GameManager.Instance.playerObject)
			{
				randX = curDist + distX + Random.Range(-20f, 20f);
			}

			whereToSpawn = new Vector3(randX, -12.3f, 0);

			var theSpawnedItem = PoolManager.GetPooledObject(spawnString);

			theSpawnedItem.transform.position = whereToSpawn;
			theSpawnedItem.transform.rotation = Quaternion.identity;
			theSpawnedItem.transform.localScale = new Vector3 (1.5f, Random.Range(1.5f,2f), 1.5f);
			theSpawnedItem.SetActive(true);

			maxDist = curDist + distX;
		}
	}
}
