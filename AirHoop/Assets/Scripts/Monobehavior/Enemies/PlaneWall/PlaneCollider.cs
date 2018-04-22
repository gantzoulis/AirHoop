using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneCollider : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == DataManager.Instance.playerColliderName)
        {
            //Destroy(other.gameObject.transform.parent.gameObject);
            other.gameObject.transform.parent.gameObject.GetComponent<Aircraft_motor>().DeathEvent();
        }
    }
}
