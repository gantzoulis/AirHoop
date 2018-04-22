using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTrigger : MonoBehaviour
{
    public int pointScore;

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == DataManager.Instance.playerColliderName)
        {
            DataManager.Instance.playerScore += pointScore;
            //deactivate this!! - Check if Stavros will find it. Har har har
            //this.gameObject.SetActive(false);
            //Must check object Pool to initiate this collider also.
        }
    }

}
