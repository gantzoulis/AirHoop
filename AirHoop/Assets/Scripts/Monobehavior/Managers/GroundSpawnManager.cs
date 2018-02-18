using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawnManager : MonoBehaviour 
{
	private float screenWidth;
	private float targetPlayerX;
	private float currentPlayerX;
	private float groundLength = 60.0f;
	private float currentEnd = 30.0f;

	[SerializeField]
	private GameObject player;
	[SerializeField]
	private GameObject groundTileGroup;

	// Use this for initialization
	void Start () 
	{
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
		currentPlayerX = player.transform.position.x;
		if (currentPlayerX >= targetPlayerX)
		{
			targetPlayerX += groundLength;
			Instantiate(groundTileGroup, new Vector3(currentEnd, -12, 0), Quaternion.identity);
			currentEnd += groundLength;
		}
	}
}