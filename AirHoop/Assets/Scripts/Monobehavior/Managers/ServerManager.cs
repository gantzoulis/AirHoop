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

    public string _playerName;
    public int _playerCash;
    public int _playerScore;

    private void Start()
    {
        _playerName = "TestUser";
        _playerCash = 1500;
        _playerScore = 0;
        _Get_PlayerData(_playerName);
    }

    public void _Get_PlayerData(string _playerName)
    {
        Debug.Log("Getting Player Data "+ _playerName);
        DataManager.Instance.playerName = _playerName;
        DataManager.Instance.playerCash = _playerCash;
        DataManager.Instance.playerScore = _playerScore;
    }
}
