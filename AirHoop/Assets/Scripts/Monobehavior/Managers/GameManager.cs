using System.Collections;
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

    
    public int playerScore = 0;

	public List<float> lvUpDistanceList = new List<float>();
	public string reachedLv;

    private void OnEnable()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    // Use this for initialization
    void Start () 
	{
			
	}
	
	// Update is called once per frame
	void Update () 
	{
        playerTimeLapseFuel += timeLapseRatio * Time.deltaTime;
    }

    public void RestartGame()
    {
        gameOver = false;
        SceneManager.LoadScene("Main");
    }


   
}
