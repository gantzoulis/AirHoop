using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEuler : MonoBehaviour
{

    [SerializeField]
    private GameObject planeObject;

    [SerializeField]
    private float eulerDisplay_X;
    [SerializeField]
    private float eulerDisplay_Y;
    [SerializeField]
    private float eulerDisplay_Z;

    [SerializeField]
    private float eulerPlay_X;
    [SerializeField]
    private float eulerPlay_Y;
    [SerializeField]
    private float eulerPlay_Z;

    // Use this for initialization
    void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        
		
	}

    public void SetupPlane_Display()
    {
        Debug.Log("DISPLAY SETTINGS");
        this.transform.eulerAngles = new Vector3(eulerDisplay_X, 180 + eulerDisplay_Y,eulerDisplay_Z);

    }

    public void SetupPlane_Play()
    {
        Debug.Log("PLAY SETTINGS");
        this.transform.localEulerAngles = new Vector3(0, 90, 0);
        //planeObject.transform.localEulerAngles = new Vector3(0,)
    }
}
