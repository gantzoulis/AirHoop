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

    [SerializeField]
    private GameObject gameOverImage;

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
        CheckGameOver();
	}

    void CheckGameOver()
    {
        if (GameManager.Instance.gameOver)
        {
            StartCoroutine(ShowGameOverText());
        }
    }

    IEnumerator ShowGameOverText()
    {
        yield return new WaitForSeconds(3);
        gameOverImage.SetActive(true);
        timeManager.pauseGame = true;
    }
}
