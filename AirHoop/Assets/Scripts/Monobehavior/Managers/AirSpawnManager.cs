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
		public float enemySpawnRateTime;
		[HideInInspector]
		public List<GameObject> buffs;
		public float buffSpawnRateTime;
		[Range(0,100)]
		public float fuelChance;
		[HideInInspector]
		public List<GameObject> fuels;
	}
		
	[SerializeField]
	private List<Level> levelList = new List<Level>();
	private Level levelClass = new Level();
	[SerializeField]
	private Level currentLevel = new Level();

	private const string ENEMY_PREFAB_PATH = "Prefabs/Enemies/FlyEnemies";
	private string enemyPrefabName;
	private string enemySpawnString;

	private const string BUFF_PREFAB_PATH = "Prefabs/Buffs/Abilities";
	private const string FUEL_PREFAB_PATH = "Prefabs/Buffs/Fuel";
	private string buffPrefabName;
	private string buffSpawnString;

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
	private float nextEnemySpawn = 0.0f;
	private float enemyRandX;
	private const float enemyDistX = 60.0f; 
	private float enemyRandY;
	private const float enemyDistY = 15.0f;
	private float maxEnemyDist = 0f;
	private float curEnemyDist;
	private float overlapEnemySize = 10.0f;
	private Vector3 whereToSpawnEnemy;

	//[SerializeField]
	private GameObject[] buffList;
	//[SerializeField]
	private List<GameObject> buffList1;
	//[SerializeField]
	private List<GameObject> buffList2;
	//[SerializeField]
	private List<GameObject> buffList3;
	//[SerializeField]
	private List<GameObject> buffList4;
	//[SerializeField]
	private List<GameObject> buffList5;
	private int randBuff;
	private GameObject spawnBuff;
	private float nextBuffSpawn = 0.0f;
	private float buffRandX;
	private const float buffDistX = 70.0f; 
	private float buffRandY;
	private const float buffDistY = 15.0f;
	private float maxBuffDist = 0f;
	private float curBuffDist;
	private float overlapBuffSize = 3.0f;
	private Vector3 whereToSpawnBuff;

	//[SerializeField]
	private GameObject[] fuelList;
	//[SerializeField]
	private List<GameObject> fuelList1;
	//[SerializeField]
	private List<GameObject> fuelList2;
	//[SerializeField]
	private List<GameObject> fuelList3;
	//[SerializeField]
	private List<GameObject> fuelList4;
	//[SerializeField]
	private List<GameObject> fuelList5;
	private int randFuel;

	void Start()
	{
		SetEnemyLists();
		SetBuffsList();
	}

	void FixedUpdate()
	{
		CheckAndSetLv();
		SpawnEnemyEngine();
		SpawnBuffEngine();
	}

	private void SetEnemyLists()
	{
		maxEnemyDist = GameManager.Instance.playerObject.transform.position.x;
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

	private void SetBuffsList()
	{
		maxEnemyDist = GameManager.Instance.playerObject.transform.position.x;
		buffList = Resources.LoadAll<GameObject>(BUFF_PREFAB_PATH);
		buffList1 = new List<GameObject>();
		buffList2 = new List<GameObject>();
		buffList3 = new List<GameObject>();
		buffList4 = new List<GameObject>();
		buffList5 = new List<GameObject>();

		for (int i = 0; i < buffList.Length; i++)
		{
			if(buffList[i].name.ToString().Contains("Lv1"))
			{
				buffList1.Add(buffList[i]);
			}
			if(buffList[i].name.ToString().Contains("Lv2"))
			{
				buffList2.Add(buffList[i]);
			}
			if(buffList[i].name.ToString().Contains("Lv3"))
			{
				buffList3.Add(buffList[i]);
			}
			if(buffList[i].name.ToString().Contains("Lv4"))
			{
				buffList4.Add(buffList[i]);
			}
			if(buffList[i].name.ToString().Contains("Lv5"))
			{
				buffList5.Add(buffList[i]);
			}
		}

		fuelList = Resources.LoadAll<GameObject>(FUEL_PREFAB_PATH);
		fuelList1 = new List<GameObject>();
		fuelList2 = new List<GameObject>();
		fuelList3 = new List<GameObject>();
		fuelList4 = new List<GameObject>();
		fuelList5 = new List<GameObject>();

		for (int i = 0; i < fuelList.Length; i++)
		{
			if(fuelList[i].name.ToString().Contains("Lv1"))
			{
				fuelList1.Add(fuelList[i]);
			}
			if(fuelList[i].name.ToString().Contains("Lv2"))
			{
				fuelList2.Add(fuelList[i]);
			}
			if(fuelList[i].name.ToString().Contains("Lv3"))
			{
				fuelList3.Add(fuelList[i]);
			}
			if(fuelList[i].name.ToString().Contains("Lv4"))
			{
				fuelList4.Add(fuelList[i]);
			}
			if(fuelList[i].name.ToString().Contains("Lv5"))
			{
				fuelList5.Add(fuelList[i]);
			}
		}

		for (int i = 0; i < levelList.Count; i++)
		{
			switch (i)
			{
			case 0:
				levelList[0].buffs = buffList1;
				levelList[0].fuels = fuelList1;
				break;
			case 1:
				levelList[1].buffs = buffList2;
				levelList[1].fuels = fuelList2;
				break;
			case 2:
				levelList[2].buffs = buffList3;
				levelList[2].fuels = fuelList3;
				break;
			case 3:
				levelList[3].buffs = buffList4;
				levelList[3].fuels = fuelList4;
				break;
			case 4:
				levelList[4].buffs = buffList5;
				levelList[4].fuels = fuelList5;
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
		if (distance > GameManager.Instance.lvUpDistanceList[0] && distance <= GameManager.Instance.lvUpDistanceList[1])
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

		GameManager.Instance.reachedLv = currentLevel.levelName;
	}

	private void SpawnEnemyEngine()
	{
		if (GameManager.Instance.playerObject)
		{
			curEnemyDist = GameManager.Instance.playerObject.transform.position.x;
		}

		if(Time.time > nextEnemySpawn && (curEnemyDist > maxEnemyDist + overlapEnemySize))
		{
			randEnemy = Random.Range(0, currentLevel.enemies.Count);
			spawnEnemy = currentLevel.enemies[randEnemy];

			enemyPrefabName = spawnEnemy.name.ToString();
			enemySpawnString = ENEMY_PREFAB_PATH + "/" + enemyPrefabName;

			nextEnemySpawn = Time.time + currentLevel.enemySpawnRateTime;

			if (GameManager.Instance.playerObject)
			{
				enemyRandX = GameManager.Instance.playerObject.transform.position.x + enemyDistX;
			}
			enemyRandY = Random.Range(GameManager.Instance.minAirplaneHeight + enemyDistY, GameManager.Instance.maxAirplaneHeight - overlapEnemySize);
			whereToSpawnEnemy = new Vector3(enemyRandX, enemyRandY, 0);

			var theSpawnedItem = PoolManager.GetPooledObject(enemySpawnString);

			theSpawnedItem.transform.position = whereToSpawnEnemy;
			theSpawnedItem.transform.rotation = Quaternion.identity;
			theSpawnedItem.SetActive(true);

			maxEnemyDist = curEnemyDist;
		}
	}

	private void SpawnBuffEngine()
	{
		if (GameManager.Instance.playerObject)
		{
			curBuffDist = GameManager.Instance.playerObject.transform.position.x;
		}

		if(Time.time > nextBuffSpawn && (curBuffDist > maxBuffDist + overlapBuffSize))
		{
			float fuelOrBuff = Random.Range(0, 100);

			if(fuelOrBuff <= currentLevel.fuelChance)
			{
				randFuel = Random.Range(0, currentLevel.fuels.Count);
				spawnBuff = currentLevel.fuels[randFuel];

				buffPrefabName = spawnBuff.name.ToString();
				buffSpawnString = FUEL_PREFAB_PATH + "/" + buffPrefabName;
			}
			else
			{
				randBuff = Random.Range(0, currentLevel.buffs.Count);
				spawnBuff = currentLevel.buffs[randBuff];

				buffPrefabName = spawnBuff.name.ToString();
				buffSpawnString = BUFF_PREFAB_PATH + "/" + buffPrefabName;
			}

			nextBuffSpawn = Time.time + currentLevel.buffSpawnRateTime;
				
			if (GameManager.Instance.playerObject)
			{
				buffRandX = GameManager.Instance.playerObject.transform.position.x + buffDistX;
			}
			buffRandY = Random.Range(GameManager.Instance.minAirplaneHeight + buffDistY, GameManager.Instance.maxAirplaneHeight - overlapBuffSize);
			whereToSpawnBuff = new Vector3(buffRandX, buffRandY, 0);

			var theSpawnedItem = PoolManager.GetPooledObject(buffSpawnString);

			theSpawnedItem.transform.position = whereToSpawnBuff;
			theSpawnedItem.transform.rotation = Quaternion.identity;
			theSpawnedItem.SetActive(true);

			maxBuffDist = curBuffDist;
		}
	}
}
