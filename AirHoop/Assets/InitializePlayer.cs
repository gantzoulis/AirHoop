using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializePlayer : MonoBehaviour
{



	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(this.gameObject);
        GameObject PlayerObject = Instantiate(GameManager.Instance.choosenAircraft.model[0]);
        PlayerObject.transform.SetParent(this.gameObject.transform);


	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
