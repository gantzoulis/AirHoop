using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirSpawnManager : MonoBehaviour 
{
	[System.Serializable]
	public class Level
	{
		public string levelName;
		[HideInInspector]
		public List<GameObject> enemies;
		public float spawnRateTime;
	}
		
	[SerializeField]
	private List<Level> levelList = new List<Level>();
	private Level levelClass = new Level();
	private Level currentLevel = new Level();

	private const string ENEMY_PREFAB_PATH = "Prefabs/Enemies/FlyEnemies";
	private string enemyPrefabName;
	private const string BUFF_PREFAB_PATH = "Prefabs/Buffs";
	private string buffPrefabName;
	private string spawnString;

	private int randSpawnedObj;
	[Range(0, 100)]
	public float buffChance;
	private float enemyChance;

	//[SerializeField]
	private GameObject[] enemyList;
	//[SerializeField]
	private List<GameObject> enemyList1;
	//[SerializeField]
	private List<GameObject> enemyList2;
	//[SerializeField]
	private List<GameObject> enemyList3;
	//[SerializeField]
	private List<GameObject> enemyList4;
	//[SerializeField]
	private List<GameObject> enemyList5;
	private int randEnemy;
	private GameObject spawnEnemy;



	[SerializeField]
	private GameObject[] buffList;
	private int randBuff;
	private GameObject spawnBuff;

	private float maxDist = 0f;
	private float curDist;
	private float overlapSize = 10.0f;

	private float randX;
	private const float distX = 60.0f; 
	private float randY;
	private const float distY = 15.0f;

	private Vector3 whereToSpawn;

	public float spawnRate = 2.5f;
	private float nextSpawn = 0.0f;

	void Start()
	{
		SetEnemyLists();
	}

	void FixedUpdate()
	{
		//SpawnEngine();
		CheckAndSetLv();
		SpawnEnemyEngine();
	}

	private void SetEnemyLists()
	{
		maxDist = GameManager.Instance.playerObject.transform.position.x;
		enemyList = Resources.LoadAll<GameObject>(ENEMY_PREFAB_PATH);
		enemyList1 = new List<GameObject>();
		enemyList2 = new List<GameObject>();
		enemyList3 = new List<GameObject>();
		enemyList4 = new List<GameObject>();
		enemyList5 = new List<GameObject>();

		for (int i = 0; i < enemyList.Length; i++)
		{
			if(enemyList[i].name.ToString().Contains("Lv1"))
			{
				enemyList1.Add(enemyList[i]);
			}
			if(enemyList[i].name.ToString().Contains("Lv2"))
			{
				enemyList2.Add(enemyList[i]);
			}
			if(enemyList[i].name.ToString().Contains("Lv3"))
			{
				enemyList3.Add(enemyList[i]);
			}
			if(enemyList[i].name.ToString().Contains("Lv4"))
			{
				enemyList4.Add(enemyList[i]);
			}
			if(enemyList[i].name.ToString().Contains("Lv5"))
			{
				enemyList5.Add(enemyList[i]);
			}
		}	

		for (int i = 0; i < levelList.Count; i++)
		{
			switch (i)
			{
			case 0:
				levelList[0].enemies = enemyList1;
				break;
			case 1:
				levelList[1].enemies = enemyList2;
				break;
			case 2:
				levelList[2].enemies = enemyList3;
				break;
			case 3:
				levelList[3].enemies = enemyList4;
				break;
			case 4:
				levelList[4].enemies = enemyList5;
				break;
			}
		}
	}

	private void CheckAndSetLv()
	{
		float distance =  GameManager.Instance.maxDistance;

		if (distance >= 0 && distance <= GameManager.Instance.lvUpDistanceList[0])
		{
			currentLevel = levelList[0];
		}
		if (distance >= GameManager.Instance.lvUpDistanceList[0] && distance <= GameManager.Instance.lvUpDistanceList[1])
		{
			currentLevel = levelList[1];
		}
		if (distance > GameManager.Instance.lvUpDistanceList[1] && distance <= GameManager.Instance.lvUpDistanceList[2])
		{
			currentLevel = levelList[2];
		}
		if (distance > GameManager.Instance.lvUpDistanceList[2] && distance <= GameManager.Instance.lvUpDistanceList[3])
		{
			currentLevel = levelList[3];
		}
		if (distance > GameManager.Instance.lvUpDistanceList[3] && distance <= GameManager.Instance.lvUpDistanceList[4])
		{
			currentLevel = levelList[4];
		}
	}

	private void SpawnEnemyEngine()
	{
		if (GameManager.Instance.playerObject)
		{
			curDist = GameManager.Instance.playerObject.transform.position.x;
		}

		if(Time.time > nextSpawn && (curDist > maxDist + overlapSize))
		{
			randEnemy = Random.Range(0, currentLevel.enemies.Count);
			spawnEnemy = currentLevel.enemies[randEnemy];

			enemyPrefabName = spawnEnemy.name.ToString();
			spawnString = ENEMY_PREFAB_PATH + "/" + enemyPrefabName;

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
