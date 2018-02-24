using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aircraft_motor : MonoBehaviour 
{
	[SerializeField]
	private Aircraft aircraft;
	private Quaternion aircraftRotation;
	[SerializeField]
	private GameObject propeler;

    [SerializeField]
    private float airPlaneStallThreshold;
    

    private bool propOn = true;
    private bool planeIsStalling = false;

    private AudioSource planeAudio;


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
        CheckAirplaneHeight();
	}

	private void AircraftMoveHorizontal()
	{
		if (gameObject)
		{
            if (!planeIsStalling)
            {
                gameObject.transform.Translate(Vector3.right * Time.deltaTime * aircraft.speed);
            }
            
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

			transform.rotation = Quaternion.Lerp(transform.rotation, aircraftRotation, aircraft.maneuver * Time.deltaTime);
		}
	}

	private void PropelerRotation()
	{
        if (propOn)
        {
            propeler.transform.Rotate(0, 5000 * Time.deltaTime, 0);
        }
      
	}

    private void CheckAirplaneHeight()
    {
        if (this.gameObject.transform.position.y >= GameManager.Instance.maxAirplaneHeight - airPlaneStallThreshold)
        {
            Debug.Log("WARNING");
        }

        if (this.gameObject.transform.position.y >= GameManager.Instance.maxAirplaneHeight)
        {
            Debug.Log("STALLING");
            aircraftRotation *= Quaternion.AngleAxis(1, Vector3.back);
            //planeIsStalling = true;
        }
    }

    private void OnDestroy()
    {
        Instantiate(GameManager.Instance.planeExplosionObject, this.transform.position, Quaternion.identity);
    }
}
