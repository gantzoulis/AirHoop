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
        GameManager.Instance.gameStart = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!GameManager.Instance.gameOver)
        {
            CheckPlayerDistance();
        }
	}

    private void CheckPlayerDistance()
    {
        playerDistanceCovered = GameManager.Instance.playerObject.transform.position.x / GameManager.Instance.distanceRatio;
        if (playerDistanceCovered >= maxDistance)
        {
            maxDistance = playerDistanceCovered;
			GameManager.Instance.maxDistance = maxDistance;
        }
    }
}
