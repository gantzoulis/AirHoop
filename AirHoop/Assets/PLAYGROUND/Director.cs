using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour
{

    public GameObject playerPlane1;
    public GameObject playerPlane2;

    // Use this for initialization
    void Start ()
    {
        Debug.Log(playerPlane1.transform.rotation.eulerAngles.x);
        Debug.Log(playerPlane1.transform.rotation.eulerAngles.y);
        Debug.Log(playerPlane1.transform.rotation.eulerAngles.z);
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}
