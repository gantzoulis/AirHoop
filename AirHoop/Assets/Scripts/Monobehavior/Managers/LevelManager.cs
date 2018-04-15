using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [SerializeField]
    private GameObject player;
    private GameObject playerSpawnObject;
    [SerializeField]
    private Transform playerSpawnPos;

    private void Awake()
    {
        SpawnPlayer();
    }

    // Use this for initialization
    void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void SpawnPlayer()
    {
        playerSpawnObject = DataManager.Instance.choosenAircraft.model[0];
        player = Instantiate(playerSpawnObject, playerSpawnPos.position, Quaternion.identity);
        Camera.main.GetComponent<MultipleTargetCamera>().targets.Add(player.transform);
        DataManager.Instance.playerObject = player;
    }
}
