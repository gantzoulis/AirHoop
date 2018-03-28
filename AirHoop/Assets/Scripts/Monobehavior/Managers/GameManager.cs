﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{

	private static GameManager instance;

	public static GameManager Instance
	{
		get 
		{
			if (instance == null)
			{
                instance = GameObject.FindObjectOfType<GameManager>();
			}
			return instance;
		}
	}
		
	public Aircraft choosenAircraft;
	public float time;
    public GameObject playerObject;
    public bool gameOver = false;
    public bool soundOn = true;
        
    public float distanceRatio;
	public float maxDistance;

	 //Remember that the default Height is 0
    public float maxAirplaneHeight;
    public float minAirplaneHeight;

    public GameObject planeExplosionObject;

    public float maxTimeLapse = 5f; //Number of seconds the player can travel back in time.
    public float playerTimeLapseFuel;
    public float timeLapseRatio; //Number of TimeFuel per Second up to 100.
    public string playerColliderName;

    public int playerLives = 1;
    public int playerScore = 0;

    public bool playerIsActive = true;
    public Vector3 playerDeathPosition;
    public Quaternion playerDeathRotation;

    public float playerRespawnXoffset;
    public float playerRespawnYoffset;

	public List<float> lvUpDistanceList = new List<float>();
	public string reachedLv;

    public Vector3 defaultPlayerSpawnPos = new Vector3(-3.3f, 0, 0);

    

    private void OnEnable()
    {
        //playerObject = GameObject.FindGameObjectWithTag("Player");
        SpawnPlayer();
    }

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Use this for initialization
    void Start () 
	{
        	
	}
	
	// Update is called once per frame
	void Update () 
	{
        playerTimeLapseFuel += timeLapseRatio * Time.deltaTime;
        if (!playerIsActive && playerLives >1)
        {
            Debug.Log("Player Is Dead");
            Vector3 newPosAfterDeath = 
                new Vector3(playerDeathPosition.x - playerRespawnXoffset, playerDeathPosition.y + playerRespawnYoffset, 0);
            StartCoroutine(RespawnPlayer(newPosAfterDeath, playerDeathRotation));
        }
    }

    public void RestartGame()
    {
        gameOver = false;
        SceneManager.LoadScene("Main");
    }

    

    public IEnumerator RespawnPlayer(Vector3 spawnPosition, Quaternion spawnRotation)
    {
        playerObject.SetActive(false);
        playerIsActive = true;
        playerLives--;
        playerObject.transform.position = spawnPosition;
        playerObject.transform.rotation = playerDeathRotation;

        yield return new WaitForSeconds(2.5f);
        playerObject.SetActive(true);
        StartCoroutine(playerObject.GetComponent<Aircraft_motor>().ExtraLifeFlasher());
    }

    public void SpawnPlayer()
    {
        GameObject playerAirplane =  Instantiate(choosenAircraft.model[0], defaultPlayerSpawnPos, Quaternion.identity);
        Camera.main.GetComponent<MultipleTargetCamera>().targets.Add(playerAirplane.transform);
        playerObject = playerAirplane;
    }
   

}
