using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{

    [SerializeField]
    public float flightDuration;

    public bool pauseGame;
    public float timeLapseRatio;

    [SerializeField]
    private GameObject timeLapseUImage;


	// Use this for initialization
	void Start ()
    {
        timeLapseRatio = timeLapseRatio / 100;
	}
	
	// Update is called once per frame
	void Update ()
    {
        GameTimer();
        CheckGameStatePaused();
        timeLapseUImage.GetComponent<Image>().fillAmount += timeLapseRatio * Time.deltaTime;
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

    public void SetPauseGame()
    {
        pauseGame = !pauseGame;
    }
}
