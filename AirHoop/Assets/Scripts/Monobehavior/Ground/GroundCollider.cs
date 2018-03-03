using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollider : MonoBehaviour 
{
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == GameManager.Instance.playerColliderName)
		{
			Destroy(other.gameObject.transform.parent.gameObject);
		}
	}
}
