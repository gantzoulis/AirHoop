using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirSpawnManager : MonoBehaviour 
{
	private const string ENEMY_PREFAB_PATH = "Prefabs/Enemies";
	private string enemyPrefabName;
	private const string BUFF_PREFAB_PATH = "Prefabs/Buffs";
	private string buffPrefabName;
	private string spawnString;

	private float randSpawnedObj;
	[Range(0, 100)]
	public float buffChance;
	private float enemyChance;

	[SerializeField]
	private GameObject[] enemyList;
	private int randEnemy;
	private GameObject spawnEnemy;

	[SerializeField]
	private GameObject[] buffList;
	private int randBuff;
	private GameObject spawnBuff;

	private float maxDist;
	private float curDist;
	private float overlapSize = 10.0f;

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
		enemyList = Resources.LoadAll<GameObject>(ENEMY_PREFAB_PATH);
		buffList = Resources.LoadAll<GameObject>(BUFF_PREFAB_PATH);
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
			randSpawnedObj = Random.Range(0, 100);

			if(randSpawnedObj <= buffChance)
			{
				randBuff = Random.Range(0, buffList.Length);
				spawnBuff = buffList[randBuff];

				buffPrefabName = spawnBuff.name.ToString();
				spawnString = BUFF_PREFAB_PATH + "/" + buffPrefabName;
			}
			else
			{
				randEnemy = Random.Range(0, enemyList.Length);
				spawnEnemy = enemyList[randEnemy];

				enemyPrefabName = spawnEnemy.name.ToString();
				spawnString = ENEMY_PREFAB_PATH + "/" + enemyPrefabName;
			}

			nextSpawn = Time.time + spawnRate;

			if (GameManager.Instance.playerObject)
			{
				randX = GameManager.Instance.playerObject.transform.position.x + distX + overlapSize;
			}
			randY = Random.Range(GameManager.Instance.minAirplaneHeight + distY, GameManager.Instance.maxAirplaneHeight - overlapSize);
			whereToSpawn = new Vector3(randX, randY, 0);

			var theSpawnedItem = PoolManager.GetPooledObject(spawnString);

			theSpawnedItem.transform.position = whereToSpawn;
			theSpawnedItem.transform.rotation = Quaternion.identity;
			theSpawnedItem.SetActive(true);

			maxDist = curDist;
		}
	}
}
