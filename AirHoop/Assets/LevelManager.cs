using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        GameObject Player = GameObject.Find("Player");
        GameObject SelectedPlayer = GameObject.Find("SelectedPlayer");
        SelectedPlayer = Player;
        Player.SetActive(false);
        
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
