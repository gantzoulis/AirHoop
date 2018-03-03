﻿using System.Collections;
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
        
    public float distanceRatio;

    //Remember that the default Height is 0
    public float maxAirplaneHeight;
    public float minAirplaneHeight;

    public GameObject planeExplosionObject;

    public float maxTimeLapse = 5f;
    public string playerColliderName;

    

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
        
	}

    public void RestartGame()
    {
        gameOver = false;
        SceneManager.LoadScene("Main");
    }

   
}
