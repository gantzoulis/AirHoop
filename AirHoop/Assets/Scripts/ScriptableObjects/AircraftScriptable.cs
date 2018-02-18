using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Aircrafts")]
public class AircraftScriptable : ScriptableObject 
{
	public string aircraftName;
	public float aircraftFuel;
	public float aircraftSpeed;
	public float aircraftManeuver;
}
