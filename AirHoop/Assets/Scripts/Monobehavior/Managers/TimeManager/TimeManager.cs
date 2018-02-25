using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{

    [SerializeField]
    public float flightDuration;

    public bool pauseGame;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        GameTimer();
        CheckGameStatePaused();
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
    }
}
