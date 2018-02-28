using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceManager : MonoBehaviour
{

    [SerializeField]
    public float playerDistanceCovered;
    public float maxDistance = 0;

	// Use this for initialization
	void Start ()
    {
        playerDistanceCovered = maxDistance;
	}
	
	// Update is called once per frame
	void Update ()
    {
        playerDistanceCovered = GameManager.Instance.playerObject.transform.position.x / GameManager.Instance.distanceRatio;
        if (playerDistanceCovered >= maxDistance)
        {
            maxDistance = playerDistanceCovered;
        }
	}
}
