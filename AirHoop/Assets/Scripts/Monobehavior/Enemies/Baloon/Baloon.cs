using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baloon : MonoBehaviour 
{
	[SerializeField] private float delay = 5.0f;

	void OnEnable()
	{
		StartCoroutine(Execute());	
	}

	private IEnumerator Execute()
	{
		yield return new WaitForSeconds(delay);
		gameObject.SetActive(false);
	}
}
