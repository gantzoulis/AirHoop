using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{

    public Transform playerObject;
    public float magnetSpeed;


	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        this.transform.position = Vector3.MoveTowards(transform.position, playerObject.position, magnetSpeed * Time.deltaTime);
	}
}
