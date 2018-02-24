using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceManager : MonoBehaviour
{

    [SerializeField]
    private float playerDistanceCovered;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        playerDistanceCovered = GameManager.Instance.playerObject.transform.position.x / GameManager.Instance.distanceRatio;
        	    	
	}
}
