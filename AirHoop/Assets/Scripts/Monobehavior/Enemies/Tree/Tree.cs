using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour 
{
	[SerializeField] private float destroyTime = 20.0f;

	void Start()
	{
		StartCoroutine(DestroyTree());
	}

	IEnumerator DestroyTree()
	{
		yield return new WaitForSeconds(destroyTime);

		gameObject.SetActive(false);
	}
}
