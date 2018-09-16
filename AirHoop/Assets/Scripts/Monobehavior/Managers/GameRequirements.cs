using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRequirements : MonoBehaviour
{
    [SerializeField]
    private GameObject[] managerRequired;
    public bool adminDebugMode = false;

    private bool debugModeOn = false;
	// Use this for initialization
	void Start ()
    {
        if (adminDebugMode)
        {
            foreach (var itemRequired in managerRequired)
            {
                if (!GameObject.Find(itemRequired.name))
                {
                    Instantiate(itemRequired);
                }
            }
            debugModeOn = true;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!debugModeOn && adminDebugMode)
        {
            foreach (var itemRequired in managerRequired)
            {
                if (!GameObject.Find(itemRequired.name))
                {
                    GameObject myManager = Instantiate(itemRequired);
                    myManager.GetComponentInChildren<ServerTalk>().GetPlayerData("gantzoulis");
                    GameObject myCanvas = GameObject.Find("UI_AirplaneSelect_Canvas");
                    myCanvas.GetComponent<UI_AirplaneSelect_Canvas>().ForcePlaneSelection();
                }
            }
            debugModeOn = true;
        }
	}
}
