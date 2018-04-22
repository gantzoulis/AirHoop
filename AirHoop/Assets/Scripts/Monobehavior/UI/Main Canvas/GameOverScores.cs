using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScores : MonoBehaviour
{
    [SerializeField]
    private GameObject distanceBonusText;
    [SerializeField]
    private GameObject timeBonusText;
    [SerializeField]
    private GameObject totalScoreText;
    [SerializeField]
    private GameObject distanceManager;
    [SerializeField]
    private GameObject timeManager;

    private void OnEnable()
    {
        float distanceScore = distanceManager.GetComponent<DistanceManager>().maxDistance;
        totalScoreText.GetComponent<Text>().text = totalScoreText.GetComponent<Text>().text + 
            " " + DataManager.Instance.playerScore.ToString();
        distanceBonusText.GetComponent<Text>().text = distanceBonusText.GetComponent<Text>().text + 
            " " + Mathf.RoundToInt(distanceScore).ToString();
        StartCoroutine(AddScores());
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator AddScores()
    {
        yield return new WaitForSeconds(3);
        Debug.Log("Adding your scores");
        int distanceIntScore = Mathf.RoundToInt(distanceManager.GetComponent<DistanceManager>().maxDistance);
        int newScore = DataManager.Instance.playerScore;
        int scoreThreshold = newScore + 15;
        while (distanceIntScore >= 0)
        {
            totalScoreText.GetComponent<Text>().text = "Total Score: " + newScore.ToString();
            newScore++;
            distanceIntScore--;
            if (newScore >= scoreThreshold)
            {
                yield return new WaitForSeconds(0.0f);
            }
            else
            {
                yield return new WaitForSeconds(0.2f);
            }
        }
        timeManager.GetComponent<TimeManager>().pauseGame = true;
    }
}
