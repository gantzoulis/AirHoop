using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingBorder : MonoBehaviour 
{
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == GameManager.Instance.playerColliderName)
		{
			other.gameObject.transform.parent.gameObject.GetComponent<Aircraft_motor>().DeathEvent();
		}
	}

}
