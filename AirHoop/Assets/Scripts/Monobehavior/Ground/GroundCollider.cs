﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollider : MonoBehaviour 
{
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == DataManager.Instance.playerColliderName)
		{
            other.gameObject.transform.parent.gameObject.GetComponent<Aircraft_motor>().DeathEvent();
			other.gameObject.transform.parent.gameObject.GetComponent<Aircraft_motor>().aircraftRotation 
			= other.gameObject.transform.parent.gameObject.GetComponent<Aircraft_motor>().defaultQuaternion;
        }
	}
}
