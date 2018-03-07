﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBody : MonoBehaviour
{


    public bool isRewinding = false;
    [SerializeField]
    private GameObject timelapseCounter;
    [SerializeField]
    private float timeLapsePower;

    List<PointInTime> pointsInTime;
    private Image timeImage;
    
    // Use this for initialization
	void Start ()
    {
        pointsInTime = new List<PointInTime>();
        if (this.gameObject.tag == "Player")
        {
            timeImage = timelapseCounter.GetComponent<Image>();
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartRewind();
        }
        if (Input.GetKeyUp(KeyCode.Return))
        {
            StopRewind();
        }
	}

    private void FixedUpdate()
    {
        if (isRewinding)
        {
            Rewind();
        }
        else
        {
            Record();
        }
    }


    void Record()
    {
        if (pointsInTime.Count > Mathf.Round(GameManager.Instance.maxTimeLapse / Time.fixedDeltaTime))
        {
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }

        pointsInTime.Insert(0, new PointInTime(transform.position,transform.rotation));
    }

    void Rewind()
    {
        if (pointsInTime.Count > 0)
        {
            PointInTime pointInTime = pointsInTime[0];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            pointsInTime.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }
    }

    public void StartRewind()
    {
        isRewinding = true;
    }

    public void StopRewind()
    {
        isRewinding = false;
    }
}
