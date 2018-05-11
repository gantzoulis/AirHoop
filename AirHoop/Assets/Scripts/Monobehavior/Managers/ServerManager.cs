using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerManager : MonoBehaviour
{
    #region Singleton Definition
    private static ServerManager instance;
    public static ServerManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<ServerManager>();
            }
            return instance;
        }
    }
    #endregion

    [SerializeField]
    private AllAircrafts airplaneList;
    
    private ServerTalk serverTalkScript;
    [Header("Player Information - From server")]
    public PlayerDataClass playerData = new PlayerDataClass();


    #region TEMP
    /*
    [Header("TEMP Info to be replaced from Player Data")]
    public string _userID;
    public string _playerName;
    public int _playerCash;
    public int _playerScore;
    */
    #endregion

    private void Start()
    {
        serverTalkScript = gameObject.GetComponent<ServerTalk>();

        /*
        _playerName = "TestUser";
        _playerCash = 1500;
        _playerScore = 0;
        _Get_PlayerData(_playerName);
        */
        BuildPlaneSelection();
        
    }

    
    public void _Get_PlayerData()
    {
        DataManager.Instance.playerName = playerData.userid;
        DataManager.Instance.playerCoins = playerData.player_coins;
        DataManager.Instance.playerTriodinium = playerData.player_triodinium;
    }
    
    private void BuildPlaneSelection()
    {
        foreach (Aircraft aircraft in airplaneList.aircrafts)
        {
            PlayerAirplaneSelection currentPlane = new PlayerAirplaneSelection();
            currentPlane.aircraft = aircraft;
            currentPlane.airPlaneCost = 0;
            currentPlane.playerOwned = true;
            DataManager.Instance.airplaneList.Add(currentPlane);
        }
    }
   
}
