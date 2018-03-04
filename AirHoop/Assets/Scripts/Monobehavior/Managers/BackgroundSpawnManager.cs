using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawnManager : MonoBehaviour 
{
	private const string CLOUD_PREFAB_PATH = "Prefabs/Backgrounds/Clouds";

	private string spawnString;

	[SerializeField]
	private  GameObject[] cloudList;
	private int randCloud;
	private GameObject spawnedCloud;
	private float targetPlayerX;
	private float currentPlayerX;
	private float cloudSpawnDist = 40.0f;
	private float screenCover = 60.0f;

	void Start()
	{
		cloudList = Resources.LoadAll<GameObject>(CLOUD_PREFAB_PATH);
		targetPlayerX = 0;
	}

	void Update()
	{
		SpawnCloud();
	}

	private void SpawnCloud()
	{
		randCloud = Random.Range(0, cloudList.Length);
		spawnedCloud = cloudList[randCloud];
		spawnString = CLOUD_PREFAB_PATH + "/" + spawnedCloud.name.ToString();

		if(GameManager.Instance.playerObject)
		{
			currentPlayerX = GameManager.Instance.playerObject.transform.position.x;
		}
		if(currentPlayerX >= targetPlayerX)
		{
			targetPlayerX += Random.Range(10,cloudSpawnDist);

			var theSpawnedItem = PoolManager.GetPooledObject(spawnString);

			theSpawnedItem.transform.position = new Vector3(screenCover + currentPlayerX, Random.Range(GameManager.Instance.minAirplaneHeight + 20, GameManager.Instance.maxAirplaneHeight), Random.Range(-5,2));
			theSpawnedItem.transform.rotation = Quaternion.identity;
			theSpawnedItem.SetActive(true);
		}
	}
}
