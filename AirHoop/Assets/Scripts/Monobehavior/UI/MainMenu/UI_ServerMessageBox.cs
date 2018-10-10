using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ServerMessageBox : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        if (ServerTalk.Instance.srvMesBoxToggle)
        {
            this.gameObject.SetActive(true);
        }	
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
