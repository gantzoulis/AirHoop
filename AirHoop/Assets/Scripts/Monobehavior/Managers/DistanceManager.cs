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
        DataManager.Instance.gameStart = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!DataManager.Instance.gameOver)
        {
            CheckPlayerDistance();
        }
	}

    private void CheckPlayerDistance()
    {
        playerDistanceCovered = DataManager.Instance.playerObject.transform.position.x / DataManager.Instance.distanceRatio;
        if (playerDistanceCovered >= maxDistance)
        {
            maxDistance = playerDistanceCovered;
			DataManager.Instance.maxDistance = maxDistance;
        }
    }
}
