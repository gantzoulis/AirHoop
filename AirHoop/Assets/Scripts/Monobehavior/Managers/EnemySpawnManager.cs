using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour 
{
	public GameObject[] enemy;

	private float randX;
	private float randY;

	private Vector3 whereToSpawn;

	public float spawnRate = 2.0f;
	private float nextSpawn = 0.0f;

	void Start()
	{
		
	}

	void Update()
	{
		
	}

	private void spawnEngine()
	{
		if(Time.time > nextSpawn)
		{
			nextSpawn = Time.time + spawnRate;
			randY = Random.Range();

		}
	}

}
