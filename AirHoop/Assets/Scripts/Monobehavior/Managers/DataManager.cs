using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    #region Singleton Definition
    private static DataManager instance;
    public static DataManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<DataManager>();
            }
            return instance;
        }
    }
    #endregion

    #region DifficultyManager
    public DifficultyLevel gameDifficultyLevel;
    #endregion

    #region ServerManager
    [Header("Server Talk Config Objects")]
    public string userID;
    public string playerName;
    public int playerCoins;
    public int playerTriodinium;
    public int playerEarnedTriodinium;
    public int playerScore;
    public int playerFinalScore;
    public float playerFlightTime;
    #endregion

    #region PlaneSelection
    public Aircraft choosenAircraft;
    [SerializeField]
    //private PlayerAirplaneSelection[] airplanePrefabs;
    public List<PlayerAirplaneSelection> airplaneList = new List<PlayerAirplaneSelection>();
    #endregion

    #region Game Management
    [Header("General Config Objects")]
    public GameObject playerObject;
    public float distanceRatio = 4.0f;
    public float distanceConvertRatio = 0.5f;
    public float maxDistance;
    public bool gameStart;
    public bool gameOver;
    public bool playerIsActive;
    public int playerLives = 1;
    [HideInInspector]
    public string playerColliderName;
    public GameObject planeExplosionObject;
    #endregion

    #region TimeLapse Management
    public float maxTimeLapseDuration = 5f; //in seconds
    public float playerTimeLapseFuel;
    public float timeLapseRatio; //Number of TimeFuel per Second up to 100.
    #endregion

    #region Player Coordinates
    [HideInInspector]
    public Vector3 defaultPlayerSpawnPos;
    public float maxAirplaneHeight = 25.0f;
    public float minAirplaneHeight = -17.0f;
    public Vector3 playerDeathPosition;
    public Quaternion playerDeathRotation;
    #endregion

    #region Leveling System
    [Header("Difficutly Level Configurator")]
    public List<float> lvUpDistanceList = new List<float>();
    //[HideInInspector]
    public string reachedLv;
    #endregion

    #region AirplaneMotor Stall
    [Header("Airplane Motor Configuration")]
    public float airPlaneStallThreshold; //15
    public float airPlaneStallManeuver; //2
    public float airPlaneStallSpeed; //12
    public float airPlaneStallAngle; //-80
    #endregion

}
