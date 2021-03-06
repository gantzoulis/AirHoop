﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour 
{
	
	private static GameObject pmEnemies = new GameObject("#EnemiesPoolingManager", typeof(PoolManager));
	private static GameObject pmBuff = new GameObject("#BuffPoolingManager", typeof(PoolManager));
	private static GameObject pmGround = new GameObject("#GroundPoolingManager", typeof(PoolManager));
	private static GameObject pmBackground = new GameObject("#BackgroundPoolingManager", typeof(PoolManager));

	private const int defaultPoolSize = 5;

	public static bool expandWhenNecessary = true;

	public static Dictionary<string, List<GameObject>> objectPools = new Dictionary<string, List<GameObject>>();

	private static bool PoolExistsForPrefab(string prefabPath)
	{
		return objectPools.ContainsKey(prefabPath);
	}

	private static bool IsAvailableForReuse(GameObject gameObject)
	{
		return !gameObject.activeSelf;	
	}

	public static void UpdateManager()
	{
		if(!pmEnemies)
		{
			pmEnemies = new GameObject("#EnemiesPoolingManager", typeof(PoolManager));
			objectPools = new Dictionary<string, List<GameObject>>();
		}

		if(!pmBuff)
		{
			pmBuff = new GameObject("#BuffPoolingManager", typeof(PoolManager));
			objectPools = new Dictionary<string, List<GameObject>>();
		}

		if(!pmGround)
		{
			pmGround = new GameObject("#GroundPoolingManager", typeof(PoolManager));
			objectPools = new Dictionary<string, List<GameObject>>();
		}

		if(!pmBackground)
		{
			pmBackground = new GameObject("#BackgroundPoolingManager", typeof(PoolManager));
			objectPools = new Dictionary<string, List<GameObject>>();
		}
	}

	private static GameObject ExpandPoolAndGetObject(string prefabPath, List<GameObject> pool)
	{
		if(!expandWhenNecessary)
		{
			return null;
		}

		GameObject prefab = Resources.Load<GameObject>(prefabPath);
		GameObject goInstance = Object.Instantiate(prefab) as GameObject;

		goInstance.name = prefab.name;

		if(prefabPath.Contains("Buffs"))
		{
			goInstance.transform.parent = pmBuff.transform;
		}
		else if(prefabPath.Contains("Enemies"))
		{
			goInstance.transform.parent = pmEnemies.transform;
		}
		else if (prefabPath.Contains("Grounds"))
		{
			goInstance.transform.parent = pmGround.transform;
		}
		else if (prefabPath.Contains("Backgrounds"))
		{
			goInstance.transform.parent = pmBackground.transform;
		}
		pool.Add(goInstance);
		return goInstance;
	}

	public static List<GameObject> CreateObjectPool(string prefabPath, int count)
	{
		if(count <= 0)
		{
			count = 1;
		}

		GameObject prefab = Resources.Load<GameObject>(prefabPath);
		List<GameObject> objects = new List<GameObject>();

		for(int i = 0; i < count; i++)
		{
			GameObject goInstance = Object.Instantiate<GameObject>(prefab);

			goInstance.name = prefab.name;

			if(prefabPath.Contains("Buffs"))
			{
				goInstance.transform.parent = pmBuff.transform;
			}
			else if(prefabPath.Contains("Enemies"))
			{
				goInstance.transform.parent = pmEnemies.transform;
			}
			else if (prefabPath.Contains("Grounds"))
			{
				goInstance.transform.parent = pmGround.transform;
			}
			else if (prefabPath.Contains("Backgrounds"))
			{
				goInstance.transform.parent = pmBackground.transform;
			}

			objects.Add(goInstance);

			goInstance.SetActive(false);
		}

		objectPools.Add(prefabPath, objects);
		return objects;
	}

	public static GameObject GetPooledObject(string prefabPath, int poolSize = defaultPoolSize)
	{
		if(!PoolExistsForPrefab(prefabPath))
		{
			return CreateAndRetrieveFromPool(prefabPath, poolSize);
		}

		var pool = objectPools[prefabPath];

		GameObject goInstance;

		return (goInstance = FindFirstAvailablePooledObject(pool)) != null ?
			goInstance: ExpandPoolAndGetObject(prefabPath, pool);
	}

	private static GameObject CreateAndRetrieveFromPool(string prefabPath, int poolSize = defaultPoolSize)
	{
		CreateObjectPool(prefabPath, poolSize);
		return GetPooledObject(prefabPath);
	}

	private static GameObject FindFirstAvailablePooledObject(List<GameObject> pool)
	{
		for(int i = 0; i < pool.Count; i++)
		{
			if(IsAvailableForReuse(pool[i]))
			{
				return pool[i];
			}
		}
		return null;
	}
}
