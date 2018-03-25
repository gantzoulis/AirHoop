using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aircraft_motor : MonoBehaviour 
{
	public Aircraft aircraft;
	private Quaternion aircraftRotation;
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
    private GameObject planeModel;

    [SerializeField]
    private Color normalColor;
    [SerializeField]
    private Color flashToColor;

    private Vector3 defaultSpawnPosition;
    [SerializeField]
    private Quaternion defaultQuaternion;


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


    void OnEnable()
	{
		aircraft = Object.Instantiate(GameManager.Instance.choosenAircraft);
        timeMachine = GetComponent<TimeBody>();
        planeAudio = GetComponent<AudioSource>();
        propelerCurrentSpeed = propelerNormalSpeed;
        defaultSpawnPosition = this.gameObject.transform.position;
 
        defaultQuaternion = this.gameObject.transform.rotation;
	}

	void Awake()
	{
		aircraft.fuel = aircraft.totalFuel;
		aircraft.speed = aircraft.manufactureSpeed;
		aircraft.maneuver = aircraft.manufactureManeuver;
		StartCoroutine(FuelUse());
	}

	void Start()
	{
		aircraftRotation = transform.rotation;
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
                gameObject.transform.Translate(Vector3.right * Time.deltaTime * aircraft.speed);
            }
            
		}
	}

	private void AircraftMoveVertical()
	{
		if (gameObject)
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
		}
	}

	private void PropelerRotation(int propelerSpeed)
	{
        if (propOn)
        {
            propeler.transform.Rotate(0, propelerSpeed * Time.deltaTime, 0);
        }
      
	}

    private void CheckAirplaneHeight()
    {
        if (this.gameObject.transform.position.y >= GameManager.Instance.maxAirplaneHeight - airPlaneStallThreshold)
        {
            //Debug.Log("WARNING");
        }

        if (this.gameObject.transform.position.y >= GameManager.Instance.maxAirplaneHeight)
        {
            //Debug.Log("STALLING");
            aircraftRotation *= Quaternion.AngleAxis(1, Vector3.back);
            //planeIsStalling = true;
        }
    }

    private void OnDestroy()
    {

        /*
        if (playerLives > 1)
        {
            Instantiate(GameManager.Instance.planeExplosionObject, this.transform.position, Quaternion.identity);
            GameObject planeSelect = GameManager.Instance.choosenAircraft.model[0];
            Instantiate(planeSelect, this.transform.position, Quaternion.identity);

        }
        else
        {
            Instantiate(GameManager.Instance.planeExplosionObject, this.transform.position, Quaternion.identity);
            GameManager.Instance.gameOver = true;
        }
        */
    }

    public void DeathEvent()
    {
        if (GameManager.Instance.playerLives > 1)
        {
            Instantiate(GameManager.Instance.planeExplosionObject, this.transform.position, Quaternion.identity);
            GameManager.Instance.playerIsActive = false;
            GameManager.Instance.playerDeathPosition = this.gameObject.transform.position;
            GameManager.Instance.playerDeathRotation = defaultQuaternion;
        }
        else
        {
            Debug.Log("GameObject " + this.gameObject.name + " Does not have any lives left. Game Over.");
            Instantiate(GameManager.Instance.planeExplosionObject, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            GameManager.Instance.playerLives--;
            GameManager.Instance.gameOver = true;
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
        Debug.Log("Flash baby");
        for (int i = 0; i < 5; i++)
        {
            planeModel.GetComponent<Renderer>().material.color = flashToColor;
            yield return new WaitForSeconds(.2f);
            planeModel.GetComponent<Renderer>().material.color = normalColor;
            yield return new WaitForSeconds(.2f);
        }
    }
    
}
