using System.Collections;
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

    private void Awake()
    {
        pointsInTime = new List<PointInTime>();
        /*
        if (this.gameObject.tag == "Player")
        {
            timeImage = timelapseCounter.GetComponent<Image>();
        }
        */
    }

    // Use this for initialization
    void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Return) && DataManager.Instance.gameOver == false)
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
        if (pointsInTime.Count > Mathf.Round(DataManager.Instance.maxTimeLapseDuration / Time.fixedDeltaTime))
        {
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }

        pointsInTime.Insert(0, new PointInTime(transform.position,transform.rotation));
    }

    void Rewind()
    {
        if (pointsInTime.Count > 0 && DataManager.Instance.playerTimeLapseFuel >= 0.03)
        {
            PointInTime pointInTime = pointsInTime[0];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            pointsInTime.RemoveAt(0);
            DataManager.Instance.playerTimeLapseFuel -= 0.2f * Time.deltaTime;
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
