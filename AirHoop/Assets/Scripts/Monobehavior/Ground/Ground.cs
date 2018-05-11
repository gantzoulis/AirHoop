using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour 
{

	[SerializeField] 
	private float destroyTime = 20.0f;
	private bool doItOnce = true;

	void Update()
	{
		if ((DataManager.Instance.playerObject.transform.position.x - this.gameObject.transform.position.x > 100) && doItOnce)
		{
			StartCoroutine(DestroyGround());
			doItOnce = false;
		}
	}

	IEnumerator DestroyGround()
	{
		yield return new WaitForSeconds(destroyTime);

		gameObject.SetActive(false);
	}
}
