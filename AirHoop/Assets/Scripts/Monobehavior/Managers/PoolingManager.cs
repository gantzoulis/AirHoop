using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour 
{

	private static PoolingManager instance;

	private static PoolingManager Instance
	{
		get
		{
			if(!instance)
			{
				new GameObject("#PoolingManager", typeof(PoolingManager));
			}
			return instance;
		}
	}

	private Dictionary<int, Queue<GameObject>> pool = new Dictionary<int, Queue<GameObject>>();

	void Awake()
	{
		if(!instance)
		{
			instance = this;
		}
		else if(instance != this)
		{
			Destroy(this);
		}
	}

	private void AddInstanceToPool(GameObject argPrefab)
	{
		int goID = argPrefab.GetInstanceID();
		argPrefab.SetActive(false);
		GameObject go = Instantiate(argPrefab);
		argPrefab.SetActive(true);
		go.name = argPrefab.name;
		go.transform.parent = transform;
		if(pool.ContainsKey(goID) == false)
		{
			pool.Add(goID, new Queue<GameObject>());
		}
		pool[goID].Enqueue(go);
	}

	private GameObject GetPooledInstance(GameObject argPrefab, Vector3 argPosition, Quaternion argRotation)
	{
		int goID = argPrefab.GetInstanceID();
		GameObject go = null;
		if(pool.ContainsKey(goID) && pool[goID].Count != 0)
		{
			if(pool[goID].Peek().activeSelf == false)
			{
				go = pool[goID].Dequeue();
				pool[goID].Enqueue(go);
				Debug.Log("HERE");
			}
			else
			{
				AddInstanceToPool(argPrefab);
				go = pool[goID].Dequeue();
				pool[goID].Enqueue(go);
				Debug.Log("HERE");
			}
		}
		else
		{
			AddInstanceToPool(argPrefab);
			go = pool[goID].Dequeue();
			pool[goID].Enqueue(go);
		}

		go.transform.position = argPosition;
		go.transform.rotation = argRotation;
		go.SetActive(true);

		return go;
	}

	public static void InstantiatePooled(GameObject argPrefab, Vector3 argPosition, Quaternion argRotation)
	{
		Instance.GetPooledInstance(argPrefab, argPosition, argRotation);
	}

	public static void InstantiatePooled<T>(GameObject argPrefab, Vector3 argPosition, Quaternion argRotation, System.Action<T> argAction)
	{
		GameObject go = Instance.GetPooledInstance(argPrefab, argPosition, argRotation);

		T[] tComponents = go.GetComponentsInChildren<T>();
		for (int i = 0; i < tComponents.Length; i++)
		{
			argAction(tComponents[i]);
		}
	}

	public static void InstantiatePooled(GameObject argPrefab, Vector3 argPosition, Quaternion argRotation, Transform argParentTo)
	{
		GameObject go = Instance.GetPooledInstance(argPrefab, argPosition, argRotation);

		if(Instance.GetComponent<Rigidbody>() != null && argParentTo.GetComponent<Rigidbody>() != null)
		{
			Debug.LogWarning("Avoid parenting rigidbodies to another rigidbody. This will cause problems. i.e.: "+go.name+".transform.parent("+argParentTo.name+".transform)");
			Debug.Break();
			return;
		}

		go.transform.SetParent(argParentTo, true);
	}
}
