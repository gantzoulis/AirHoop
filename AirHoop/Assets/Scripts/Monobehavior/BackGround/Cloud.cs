using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour 
{
	
	[SerializeField] private float minSpeed = 0.0f;
	[SerializeField] private float maxSpeed = 2.0f;
	[SerializeField] private float destroyTime = 20.0f;
	private float randSpeed;
	private bool doItOnce = true;

	void Start()
	{
		randSpeed = Random.Range(minSpeed, maxSpeed);
	}

	void Update()
	{
		CloudSpeed();
        if (!DataManager.Instance.gameOver)
        {
            if ((DataManager.Instance.playerObject.transform.position.x - this.gameObject.transform.position.x > 100) && doItOnce)
            {
                StartCoroutine(DestroyCloud());
                doItOnce = false;
            }
        }
		
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
