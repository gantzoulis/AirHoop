using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetTrigger : MonoBehaviour
{

    public float magnetSetSpeed;
    public GameObject currentTarget;
    
    // Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        other.gameObject.GetComponent<Magnet>().magnetSpeed = magnetSetSpeed;
    }
}
