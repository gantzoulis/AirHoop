using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Aircraft_motor : MonoBehaviour 
{
	public Aircraft aircraft;
	public Quaternion aircraftRotation;
	[SerializeField]
	private GameObject propeler;
    [SerializeField]
    private GameObject smokeTrail;
    [SerializeField]
    private Color normalSmokeColor;
    [SerializeField]
    private Color speedSmokeColor;
    [SerializeField]
    private Color maneuverSmokeColor;
    [SerializeField]
    private Color outofFuelSmokeColor;
    [SerializeField]
    private bool playerImmune = false;

    
    [SerializeField]
    private GameObject planeModel;

    [SerializeField]
    private Color normalColor;
    [SerializeField]
    private Color flashToColor;

    private Vector3 defaultSpawnPosition;
    public Quaternion defaultQuaternion;


    public bool useFuel = true;
	public float fuelUsePerMeter = 0.02f;

	public bool speedBuffOn = false;
	//[SerializeField]
	private float speedBuffTime;
	//[SerializeField]
	private float speedCountBuffTime;

	public bool maneuverBuffOn = false;
	private float maneuverBuffTime;
	private float maneuverCountBuffTime;

    [SerializeField]
    private float airPlaneStallThreshold;
    private int propelerCurrentSpeed;
    private int propelerNormalSpeed = 5000;
    private int propelerFastSpeed = 9000;
    private int propelerSlowSpeed = 1000;

    private bool propOn = true;

    private bool planeIsStalling = false;

    private AudioSource planeAudio;
    private TimeBody timeMachine;

    private bool outOfFuel = false;

	[SerializeField]
	private float angleSoFar;
	private float angleLastFrame;
	[SerializeField]
	private int loopScore;

	private GameObject bloodPressure;

    void OnEnable()
	{
		aircraft = Object.Instantiate(DataManager.Instance.choosenAircraft);
        aircraft.fuel = aircraft.totalFuel;
        aircraft.speed = aircraft.manufactureSpeed;
        aircraft.maneuver = aircraft.manufactureManeuver;
        timeMachine = GetComponent<TimeBody>();
        planeAudio = GetComponent<AudioSource>();
        propelerCurrentSpeed = propelerNormalSpeed;
        defaultSpawnPosition = this.gameObject.transform.position;
        defaultQuaternion = this.gameObject.transform.rotation;
	}

	void Awake()
	{
		//aircraft.fuel = aircraft.totalFuel;
		//aircraft.speed = aircraft.manufactureSpeed;
		//aircraft.maneuver = aircraft.manufactureManeuver;
		StartCoroutine(FuelUse());
	}

	void Start()
	{
		aircraftRotation = transform.rotation;
		bloodPressure = GameObject.Find("Stall Active");
	}

	void Update()
	{
        //Debug.Log(this.transform.rotation.eulerAngles.z);
        if (!timeMachine.isRewinding)
        {
            AircraftMoveHorizontal();
            AircraftMoveVertical();
        }
		PropelerRotation(propelerCurrentSpeed);
        CheckAirplaneHeight();
        CheckAirplaneFuel();

    }

	private void AircraftMoveHorizontal()
	{
		if (gameObject)
		{
            if (!planeIsStalling)
            {
                //Debug.Log("Move Hor " + "Aircraft Spead: " + aircraft.speed);
                gameObject.transform.Translate(Vector3.right * Time.deltaTime * aircraft.speed);
            }
		}
	}

	private void AircraftMoveVertical()
	{
		if (gameObject)
		{
			if (!planeIsStalling)
			{
				
				if (Input.GetKey(KeyCode.DownArrow) && !outOfFuel)
				{
					aircraftRotation *= Quaternion.AngleAxis(aircraft.maneuver, Vector3.back);
				}

				if (Input.GetKey(KeyCode.UpArrow) && !outOfFuel)
				{
					aircraftRotation *= Quaternion.AngleAxis(aircraft.maneuver, Vector3.forward);
				}
				transform.rotation = Quaternion.Lerp(transform.rotation, aircraftRotation, 8 * Time.deltaTime);

				/*
				if (Input.GetKeyDown(KeyCode.DownArrow) || (Input.GetKeyDown(KeyCode.UpArrow)))
				{
					StartCountTurn();
				}*/

				if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow))
				{
					CountTurn();
				}
				else
				{
					StartCountTurn();
				}
			}
			else
			{
				aircraftRotation = Quaternion.Euler(0, 0, DataManager.Instance.airPlaneStallAngle);
				//angleSoFar = 0f;
			}
		}
	}

	private void StartCountTurn()
	{
		angleSoFar = 0f;
		angleLastFrame = this.transform.eulerAngles.z;
		angleLastFrame = (angleLastFrame > 180) ? 360 - angleLastFrame : angleLastFrame;
	}

	private void CountTurn()
	{
		float angle = this.transform.eulerAngles.z;
		angle = (angle > 180) ? 360 - angle : angle;

		angleSoFar += Mathf.Abs(angle - angleLastFrame);
		angleLastFrame = angle;

		if(angleSoFar > 360)
		{
			//Debug.Log("GRATZ For The Loop");
            AudioSource loopSound = GameObject.Find("LoopSound").GetComponent<AudioSource>();
            loopSound.Play();

			DataManager.Instance.playerScore += loopScore;
			angleSoFar = 0f;
		}
	}

	private void PropelerRotation(int propelerSpeed)
	{
        if (propOn)
        {
            propeler.transform.Rotate(0, propelerSpeed * Time.deltaTime, 0);
            //propeler.transform.Rotate(propelerSpeed * Time.deltaTime, 0, 0, Space.Self);
        }
      
	}

    private void CheckAirplaneHeight()
    {
		if (this.gameObject.transform.position.y >= DataManager.Instance.airPlaneStallThreshold)
        {
			if(!planeIsStalling)
			{
            	//Debug.Log("WARNING");
			}
			else
			{
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0,0, DataManager.Instance.airPlaneStallAngle), DataManager.Instance.airPlaneStallManeuver * Time.deltaTime);
				gameObject.transform.Translate(Vector3.right * DataManager.Instance.airPlaneStallSpeed * Time.deltaTime);
			}

			float stallingPerCent = (gameObject.transform.position.y - DataManager.Instance.airPlaneStallThreshold) 
				/ (DataManager.Instance.maxAirplaneHeight - DataManager.Instance.airPlaneStallThreshold);

			Image fadeOut = bloodPressure.GetComponent<Image>();
			var tempColor = fadeOut.color;
			tempColor.a = stallingPerCent;
			fadeOut.color = tempColor;
        }
		else
		{
			planeIsStalling = false;
		}

        if (this.gameObject.transform.position.y >= DataManager.Instance.maxAirplaneHeight)
        {
            //Debug.Log("STALLING");
			planeIsStalling = true;
        }

		if (this.gameObject.transform.position.y <= DataManager.Instance.minAirplaneHeight - 0.5)
		{
			this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
		}
    }


    public void DeathEvent()
    {
        if (!playerImmune)
        {
            if (DataManager.Instance.playerLives > 1)
            {
                Instantiate(DataManager.Instance.planeExplosionObject, this.transform.position, Quaternion.identity);
                DataManager.Instance.playerIsActive = false;
                DataManager.Instance.playerDeathPosition = this.gameObject.transform.position;
                DataManager.Instance.playerDeathRotation = defaultQuaternion;
				aircraftRotation = defaultQuaternion;
            }
            else
            {
                Debug.Log("GameObject " + this.gameObject.name + " Does not have any lives left. Game Over.");
                Instantiate(DataManager.Instance.planeExplosionObject, this.transform.position, Quaternion.identity);
                Destroy(this.gameObject);
                DataManager.Instance.playerLives--;
                DataManager.Instance.gameOver = true;
            }
        }
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
			speedCountBuffTime = 0;
		}
	}

	private IEnumerator SpeedBuffTime(float speedBuff, float buffTime)
	{
		speedBuffOn = true;
		speedBuffTime = buffTime;
		speedCountBuffTime = 0;
		float totalSpeed = aircraft.speed += speedBuff;
        ParticleSystem smokeTrailSystem = smokeTrail.GetComponent<ParticleSystem>();
        var main = smokeTrailSystem.main;

        while (speedCountBuffTime < speedBuffTime)
		{
            main.startColor = speedSmokeColor;
            speedCountBuffTime += Time.deltaTime;
			aircraft.speed = totalSpeed;
            propelerCurrentSpeed = propelerFastSpeed;
            PropelerRotation(propelerCurrentSpeed);
            planeAudio.pitch = 1.25f;
			yield return null;
		}
		speedCountBuffTime = 0;
		aircraft.speed = aircraft.manufactureSpeed;
        propelerCurrentSpeed = propelerNormalSpeed;
        PropelerRotation(propelerCurrentSpeed);
        main.startColor = normalSmokeColor;
        planeAudio.pitch = 1.0f;
        speedBuffOn = false;
	}

	public void ActivateManeuverBuff(float maneuverBuff, float buffTime)
	{
		if(maneuverBuffOn == false )
		{
			StartCoroutine(ManeuverBuffTime(maneuverBuff, buffTime));
		}
		if(maneuverBuffOn == true)
		{
			maneuverCountBuffTime = 0;
		}
	}

	private IEnumerator ManeuverBuffTime(float maneuverBuff, float buffTime)
	{
		maneuverBuffOn = true;
		maneuverBuffTime = buffTime;
		maneuverCountBuffTime = 0;
		float totalManeuver = aircraft.maneuver += maneuverBuff;
        ParticleSystem smokeTrailSystem = smokeTrail.GetComponent<ParticleSystem>();
        var main = smokeTrailSystem.main;

        while (maneuverCountBuffTime < maneuverBuffTime)
		{
            main.startColor = maneuverSmokeColor;
            maneuverCountBuffTime += Time.deltaTime;
			aircraft.maneuver = totalManeuver;
			propelerCurrentSpeed = propelerFastSpeed;
			PropelerRotation(propelerCurrentSpeed);
			planeAudio.pitch = 1.25f;
			yield return null;
		}
		maneuverCountBuffTime = 0;
		aircraft.maneuver = aircraft.manufactureManeuver;
		propelerCurrentSpeed = propelerNormalSpeed;
		PropelerRotation(propelerCurrentSpeed);
        main.startColor = normalSmokeColor;
        planeAudio.pitch = 1.0f;
		maneuverBuffOn = false;
	}

    private void CheckAirplaneFuel()
    {
        if (this.aircraft.fuel <= 0)
        {
            //Debug.Log("Out of Fuel");
            ParticleSystem smokeTrailSystem = smokeTrail.GetComponent<ParticleSystem>();
            var main = smokeTrailSystem.main;
            outOfFuel = true;
            this.gameObject.GetComponent<Animator>().enabled = true;
            main.startColor = outofFuelSmokeColor;
        }
    }

    public IEnumerator ExtraLifeFlasher()
    {
        //Debug.Log("Flash baby");
        playerImmune = true;
        for (int i = 0; i < 5; i++)
        {
            planeModel.GetComponent<Renderer>().material.color = flashToColor;
            yield return new WaitForSeconds(.2f);
            planeModel.GetComponent<Renderer>().material.color = normalColor;
            yield return new WaitForSeconds(.2f);
        }
        playerImmune = false;
    }
    
}
