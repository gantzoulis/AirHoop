using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawnManager : MonoBehaviour 
{
	private const string CLOUD_PREFAB_PATH = "Prefabs/Backgrounds/Clouds";
	private const string BACKMOUNTAIN_PREFAB_PATH = "Prefabs/Backgrounds/Mountains/BackMountains";
	private const string FRONTMOUNTAIN_PREFAB_PATH = "Prefabs/Backgrounds/Mountains/FrontMountains";

	//private string spawnString;

	[SerializeField] private  GameObject[] cloudList;
	private int randCloud;
	private GameObject spawnedCloud;
	private float cloudSpawnDist = 40.0f;
	private float screenCover = 60.0f;
	private float targetPlayerX;

	[SerializeField] private GameObject[] backMountainList;
	private int randBackMountain;
	private GameObject spawnedBackMountain;
	private float targetBackmountainX;
	private float backMountainLength = 120.0f;

	[SerializeField] private GameObject[] frontMountainList;
	private int randFrontMountain;
	private GameObject spawnedFrontMountain;
	private float targetFrontMountainX;

	private float currentPlayerX;
	private float randSizeX;
	private float randSizeY;


	void Start()
	{
		cloudList = Resources.LoadAll<GameObject>(CLOUD_PREFAB_PATH);
		backMountainList = Resources.LoadAll<GameObject>(BACKMOUNTAIN_PREFAB_PATH);
		frontMountainList = Resources.LoadAll<GameObject>(FRONTMOUNTAIN_PREFAB_PATH);
		targetPlayerX = 0;
		targetBackmountainX = 0;
	}

	void Update()
	{
		SpawnCloud();
		SpawnBackMountains();
		SpawnFrontMountains();
	}

	private void SpawnCloud()
	{
		randCloud = Random.Range(0, cloudList.Length);
		spawnedCloud = cloudList[randCloud];
		string spawnString = CLOUD_PREFAB_PATH + "/" + spawnedCloud.name.ToString();

		if(GameManager.Instance.playerObject)
		{
			currentPlayerX = GameManager.Instance.playerObject.transform.position.x;
		}
		if(currentPlayerX >= targetPlayerX)
		{
			targetPlayerX += Random.Range(10,cloudSpawnDist);
			randSizeX = Random.Range(0.5f, 1.5f);
			randSizeY = Random.Range(0.5f, 1.3f);

			var theSpawnedItem = PoolManager.GetPooledObject(spawnString);

			theSpawnedItem.transform.position = new Vector3(screenCover + currentPlayerX, Random.Range(GameManager.Instance.minAirplaneHeight + 20, GameManager.Instance.maxAirplaneHeight), Random.Range(-5,2));
			theSpawnedItem.transform.rotation = Quaternion.identity;
			theSpawnedItem.transform.localScale = new Vector3(randSizeX, randSizeY, 0);
			theSpawnedItem.SetActive(true);
		}
	}

	private void SpawnBackMountains()
	{
		randBackMountain = Random.Range(0, backMountainList.Length);
		spawnedBackMountain = backMountainList[randBackMountain];
		string spawnString = BACKMOUNTAIN_PREFAB_PATH + "/" + spawnedBackMountain.name.ToString();

		if(GameManager.Instance.playerObject)
		{
			currentPlayerX = GameManager.Instance.playerObject.transform.position.x;
		}
	
		if (currentPlayerX >= targetBackmountainX)
		{
			var theSpawnedItem = PoolManager.GetPooledObject(spawnString);

			theSpawnedItem.transform.position = new Vector3(currentPlayerX + backMountainLength/2, -14f, 10f + Random.Range(-1f, 1f));
			theSpawnedItem.transform.rotation = Quaternion.identity;
			theSpawnedItem.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
			theSpawnedItem.SetActive(true);

			targetBackmountainX += backMountainLength/3;
		}
	}

	private void SpawnFrontMountains()
	{
		randFrontMountain = Random.Range(0, frontMountainList.Length);
		spawnedFrontMountain = frontMountainList[randFrontMountain];
		string spawnString = FRONTMOUNTAIN_PREFAB_PATH + "/" + spawnedFrontMountain.name.ToString();

		if(GameManager.Instance.playerObject)
		{
			currentPlayerX = GameManager.Instance.playerObject.transform.position.x;
		}
		if(currentPlayerX >= targetFrontMountainX)
		{
			targetFrontMountainX += Random.Range(40,200);
			randSizeX = Random.Range(1.5f, 3);
			randSizeY = Random.Range(1.5f, 2.5f);

			var theSpawnedItem = PoolManager.GetPooledObject(spawnString);

			theSpawnedItem.transform.position = new Vector3(screenCover + currentPlayerX, -15f, 15f);
			theSpawnedItem.transform.rotation = Quaternion.identity;
			theSpawnedItem.transform.localScale = new Vector3(randSizeX, randSizeY, 0);
			theSpawnedItem.SetActive(true);
		}
	}
}
