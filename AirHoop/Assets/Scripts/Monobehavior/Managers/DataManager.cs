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
    public string userID;
    public string playerName;
    public int playerCoins;
    public int playerTriodinium;
    public int playerScore;
    public int playerFinalScore;
    #endregion

    #region PlaneSelection
    public Aircraft choosenAircraft;
    [SerializeField]
    //private PlayerAirplaneSelection[] airplanePrefabs;
    public List<PlayerAirplaneSelection> airplaneList = new List<PlayerAirplaneSelection>();
    #endregion

    #region Game Management
    public GameObject playerObject;
    public float distanceRatio = 4.0f;
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
    public List<float> lvUpDistanceList = new List<float>();
    //[HideInInspector]
    public string reachedLv;
    #endregion
}
