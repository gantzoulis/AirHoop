using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffType
{
	Fuel, Speed
}

public class Buff : ScriptableObject
{
	public string buffName;
	public GameObject model;
	public BuffType type;
	public float amount;
	public float time;
	public int hash;
}
