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

    #region TEMP
    public string _userID;
    public string _playerName;
    public int _playerCash;
    public int _playerScore;
    #endregion

    private void Start()
    {
        serverTalkScript = gameObject.GetComponent<ServerTalk>();

        _playerName = "TestUser";
        _playerCash = 1500;
        _playerScore = 0;
        _Get_PlayerData(_playerName);
        BuildPlaneSelection();
        
    }

    public void _Get_PlayerData(string _playerName)
    {
        Debug.Log("Getting Player Data "+ _playerName);
        DataManager.Instance.playerName = _playerName;
        DataManager.Instance.playerCash = _playerCash;
        DataManager.Instance.playerScore = _playerScore;
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
