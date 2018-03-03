﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aircraft_motor : MonoBehaviour 
{
	public Aircraft aircraft;
	private Quaternion aircraftRotation;
	[SerializeField]
	private GameObject propeler;

	public bool useFuel = true;
	public float fuelUsePerMeter = 0.1f;

	public bool speedBuffOn = false;
	//[SerializeField]
	private float speedBuffTime;
	//[SerializeField]
	private float countBuffTime;

    [SerializeField]
    private float airPlaneStallThreshold;
    

    private bool propOn = true;
    private bool planeIsStalling = false;

    private AudioSource planeAudio;
    private TimeBody timeMachine;


    void OnEnable()
	{
		aircraft = Object.Instantiate(GameManager.Instance.choosenAircraft);
        timeMachine = GetComponent<TimeBody>();
	}

	void Awake()
	{
		aircraft.fuel = aircraft.totalFuel;
		aircraft.speed = aircraft.manufactureSpeed;
		StartCoroutine(FuelUse());
	}

	void Start()
	{
		aircraftRotation = transform.rotation;
	}

	void Update()
	{
        if (!timeMachine.isRewinding)
        {
            AircraftMoveHorizontal();
            AircraftMoveVertical();
        }
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
        GameManager.Instance.gameOver = true;
    }

	private IEnumerator FuelUse()
	{
		while(useFuel)
		{
			yield return new WaitForSeconds(1);
			float distancePerSecond = aircraft.speed;
			aircraft.fuel -= distancePerSecond * fuelUsePerMeter;
		}
	}
		
	public void ActivateSpeedBuff(float speedBuff, float buffTime)
	{
		if(speedBuffOn == false )
		{
			StartCoroutine(SpeedBuffTime(speedBuff, buffTime));
		}
		if(speedBuffOn == true)
		{
			countBuffTime = 0;
		}
	}

	private IEnumerator SpeedBuffTime(float speedBuff, float buffTime)
	{
		speedBuffOn = true;
		speedBuffTime = buffTime;
		countBuffTime = 0;
		float totalSpeed = aircraft.speed += speedBuff;

		while(countBuffTime < speedBuffTime)
		{
			countBuffTime += Time.deltaTime;
			aircraft.speed = totalSpeed;
			yield return null;
		}
		countBuffTime = 0;
		aircraft.speed = aircraft.manufactureSpeed;
		speedBuffOn = false;
	}
}
