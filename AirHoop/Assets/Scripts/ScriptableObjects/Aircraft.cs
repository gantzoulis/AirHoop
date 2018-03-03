using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aircraft : ScriptableObject 
{
	public string aircraftName;
	public GameObject[] model;
	public float totalFuel;
	public float fuel;
	public float manufactureSpeed;
	public float speed;
	public float maneuver;
	public int hash;
}