using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTrigger : MonoBehaviour
{
    public int pointScore;

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerCollider")
        {
            Debug.Log("Points awarded");
            GameManager.Instance.playerScore += pointScore;
        }
    }

}
