﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffGiver : MonoBehaviour 
{
	public Buff buff;

	private float addedBuffAmount;

	void Start()
	{
		addedBuffAmount = buff.amount;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			AddBuffAmount(other.gameObject.transform.parent.gameObject);
			DeactivateBuffItem();
		}
	}

	private void AddBuffAmount(GameObject go)
	{
		switch(buff.type)
		{
		case BuffType.Fuel:
			float newFuel = go.GetComponent<Aircraft_motor>().aircraft.fuel += addedBuffAmount;
			if(newFuel <= go.GetComponent<Aircraft_motor>().aircraft.totalFuel)
			{
				go.GetComponent<Aircraft_motor>().aircraft.fuel = newFuel;
				break;
			}
			else
			{
				go.GetComponent<Aircraft_motor>().aircraft.fuel = go.GetComponent<Aircraft_motor>().aircraft.totalFuel;
				break;
			}

		case BuffType.Speed:
			go.GetComponent<Aircraft_motor>().aircraft.speed += addedBuffAmount;
			break;
		}
	}

	private void DeactivateBuffItem()
	{
		this.gameObject.transform.parent.gameObject.SetActive(false);
	}
}
