using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour 
{
	private static ObjectPooler instance;

	public static ObjectPooler Instance
	{
		get
		{
			if(!instance)
			{
				instance = GameObject.FindObjectOfType<ObjectPooler>();
				new GameObject("#PoolingManager", typeof(ObjectPooler));
			}
			return instance;
		}
	}

	private const int defaultPoolSize = 10;

	public bool expandWhenNecessary = true;

	public Dictionary<string, List<GameObject>> objectPools = new Dictionary<string, List<GameObject>>();

	private bool PoolExistsForPrefab(string prefabPath)
	{
		return objectPools.ContainsKey(prefabPath);
	}

	private bool IsAvailableForReuse(GameObject gameObject)
	{
		return !gameObject.activeSelf;	
	}


	private GameObject ExpandPoolAndGetObject(string prefabPath, List<GameObject> pool)
	{
		if(!expandWhenNecessary)
		{
			return null;
		}

		GameObject prefab = Resources.Load<GameObject>(prefabPath);
		GameObject goInstance = Object.Instantiate(prefab) as GameObject;

		pool.Add(goInstance);
		return goInstance;
	}

	public List<GameObject> CreateObjectPool(string prefabPath, int count)
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

			objects.Add(goInstance);

			goInstance.SetActive(false);
		}

		objectPools.Add(prefabPath, objects);
		return objects;
	}

	public GameObject GetPooledObject(string prefabPath, int poolSize = defaultPoolSize)
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

	private GameObject CreateAndRetrieveFromPool(string prefabPath, int poolSize = defaultPoolSize)
	{
		CreateObjectPool(prefabPath, poolSize);
		return GetPooledObject(prefabPath);
	}

	private GameObject FindFirstAvailablePooledObject(List<GameObject> pool)
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
