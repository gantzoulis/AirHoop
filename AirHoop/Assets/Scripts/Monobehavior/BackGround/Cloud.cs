using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour 
{
	
	[SerializeField] private float minSpeed = 0.0f;
	[SerializeField] private float maxSpeed = 2.0f;
	[SerializeField] private float destroyTime = 10.0f;
	private float randSpeed;

	void Start()
	{
		randSpeed = Random.Range(minSpeed, maxSpeed);
		StartCoroutine(DestroyCloud());
	}

	void Update()
	{
		CloudSpeed();
	}

	private void CloudSpeed()
	{
		gameObject.transform.Translate(Vector3.left * Time.deltaTime * randSpeed);
	}

	IEnumerator DestroyCloud()
	{
		yield return new WaitForSeconds(destroyTime);

		gameObject.SetActive(false);
	}
}
