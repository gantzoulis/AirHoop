using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aircraft_motor : MonoBehaviour 
{
	[SerializeField]
	private AircraftScriptable aircraft;
	private Quaternion aircraftRotation;
	[SerializeField]
	private GameObject propeler;

	void OnEnable()
	{
		aircraft = Object.Instantiate(GameManager.Instance.choosenAircraft);
	}

	void Start()
	{
		aircraftRotation = transform.rotation;
	}

	void Update()
	{
		AircraftMoveHorizontal();
		AircraftMoveVertical();
		PropelerRotation();
	}

	private void AircraftMoveHorizontal()
	{
		if (gameObject)
		{
			gameObject.transform.Translate(Vector3.right * Time.deltaTime * aircraft.aircraftSpeed);
		}
	}

	private void AircraftMoveVertical()
	{
		if (gameObject)
		{
			if (Input.GetKey(KeyCode.DownArrow))
			{
				aircraftRotation *= Quaternion.AngleAxis(1, Vector3.back);
			}

			if (Input.GetKey(KeyCode.UpArrow))
			{
				aircraftRotation *= Quaternion.AngleAxis(1, Vector3.forward);
			}

			transform.rotation = Quaternion.Lerp(transform.rotation, aircraftRotation, aircraft.aircraftManeuver * Time.deltaTime);
		}
	}

	private void PropelerRotation()
	{
		propeler.transform.Rotate(0, 5000 * Time.deltaTime, 0);
	}
}
