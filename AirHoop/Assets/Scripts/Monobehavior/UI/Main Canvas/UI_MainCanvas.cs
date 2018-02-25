using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainCanvas : MonoBehaviour
{

    private TimeManager timeManager;
    private DistanceManager distanceManager;

    [SerializeField]
    private Text distanceText;
    [SerializeField]
    private Text timeText;

    // Use this for initialization
    void Start ()
    {
        timeManager = GameObject.Find("TimeManager").gameObject.GetComponent<TimeManager>();
        distanceManager = GameObject.Find("DistanceManager").gameObject.GetComponent<DistanceManager>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        timeText.text = Mathf.RoundToInt(timeManager.flightDuration).ToString();
        distanceText.text = Mathf.RoundToInt(distanceManager.maxDistance).ToString();
	}
}
