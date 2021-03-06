﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{

    [SerializeField]
    public float flightDuration;

    public bool pauseGame;
    public float timeLapseRatio; 
    public float timeConsumption;

    [SerializeField]
    private GameObject timeLapseUImage;


	// Use this for initialization
	void Start ()
    {
        timeLapseRatio = DataManager.Instance.playerTimeLapseFuel;
       //timeLapseRatio = timeLapseRatio / 100; //We devide with 100 to get the decimal for the fillamount of the respective Image.
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!DataManager.Instance.gameOver)
        {
            GameTimer();
        }
        CheckGameStatePaused();
        timeLapseRatio = DataManager.Instance.playerTimeLapseFuel;
        timeLapseUImage.GetComponent<Image>().fillAmount = timeLapseRatio;
        timeConsumption = CalcTimeConsumption();
	}

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    private void CheckGameStatePaused()
    {
        if (pauseGame)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    private void GameTimer()
    {
        flightDuration += Time.deltaTime;
        DataManager.Instance.playerFlightTime = flightDuration;
    }

    public void SetPauseGame()
    {
        pauseGame = !pauseGame;
    }

    private float CalcTimeConsumption()
    {
        timeConsumption = 1 / DataManager.Instance.maxTimeLapseDuration;
        return timeConsumption;
    }
}
