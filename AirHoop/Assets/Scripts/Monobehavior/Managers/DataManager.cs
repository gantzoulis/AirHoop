﻿using System.Collections;
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
    public string playerName;
    public int playerCash;
    public int playerScore;
    #endregion

    #region PlaneSelection
    public Aircraft choosenAircraft;
    [SerializeField]
    //private PlayerAirplaneSelection[] airplanePrefabs;
    public List<PlayerAirplaneSelection> airplaneList = new List<PlayerAirplaneSelection>();
    #endregion

    #region Game Management
    public GameObject playerObject;
    #endregion
}
