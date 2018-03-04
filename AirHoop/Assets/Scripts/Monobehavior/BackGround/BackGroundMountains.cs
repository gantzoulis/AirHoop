using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMountains : MonoBehaviour 
{
	[SerializeField] float destroyTime = 10.0f;

	void Start()
	{
		StartCoroutine(DestroyMountain());
	}

	IEnumerator DestroyMountain()
	{
		yield return new WaitForSeconds(destroyTime);

		gameObject.SetActive(false);
	}
}
