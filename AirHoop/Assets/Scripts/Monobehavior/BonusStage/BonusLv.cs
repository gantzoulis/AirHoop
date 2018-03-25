using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusLv : MonoBehaviour 
{
	private float counting;

	void Update()
	{
		counting = GameObject.Find("SpawnManager").GetComponent<AirSpawnManager>().coundDownSpecialTime; 

		if (counting <= 0)
		{
			this.gameObject.SetActive(false);
		}
	}
}
