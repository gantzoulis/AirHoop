using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawnManager : MonoBehaviour 
{
	private const string GROUND_PREFAB_PATH = "Prefabs/Grounds";

	private float screenWidth;
	private float targetPlayerX;
	private float currentPlayerX;
	private float groundLength = 60.0f;
	private float currentEnd = 30.0f;

	[SerializeField]
	private GameObject[] groundList;

	private int randGround;
	private GameObject spawnedGround;
	private string spawnString;

	// Use this for initialization
	void Start () 
	{
		groundList = Resources.LoadAll<GameObject>(GROUND_PREFAB_PATH);
		screenWidth = Screen.width;
		targetPlayerX = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		SpawnGround();
	}

	private void SpawnGround()
	{
		randGround = Random.Range(0, groundList.Length);
		spawnedGround = groundList[randGround];
		spawnString = GROUND_PREFAB_PATH + "/" + spawnedGround.name.ToString();

		if (GameManager.Instance.playerObject)
		{
			currentPlayerX = GameManager.Instance.playerObject.transform.position.x;
		}

		if (currentPlayerX >= targetPlayerX)
		{
			targetPlayerX += groundLength;

			var theSpawnedItem = PoolManager.GetPooledObject(spawnString);
			//var theSpwanedItem = PoolingManager.Instance.GetPooledObject(spawnString);

			theSpawnedItem.transform.position = new Vector3(currentEnd, -12, 0);
			theSpawnedItem.transform.rotation = Quaternion.identity;
			theSpawnedItem.SetActive(true);

			currentEnd += groundLength;
		}
	}
}