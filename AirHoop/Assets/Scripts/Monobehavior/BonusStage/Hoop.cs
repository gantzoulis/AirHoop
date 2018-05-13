using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoop : MonoBehaviour
{
    public bool specialHoop = false;
    public bool passedHoop = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == DataManager.Instance.playerColliderName)
        {
            passedHoop = true;
        }
    }

    public void ResetHoop()
    {
        passedHoop = false;
    }
}
