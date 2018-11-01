using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainCanvas : MonoBehaviour
{

    private TimeManager timeManager;
    private DistanceManager distanceManager;
	private AirEnemyManager spawnManager;

    [SerializeField]
    private Text distanceText;
    [SerializeField]
    private Text timeText;
	[SerializeField]
	private Text bonusLvTimeText;
    [SerializeField]
    private GameObject scoreUIElement;

    [SerializeField]
    private GameObject gameOverImage;

    [SerializeField]
    private GameObject soundOffImage;

    [SerializeField]
    private bool isMute;

    // Use this for initialization
    void Start ()
    {
        timeManager = GameObject.Find("TimeManager").gameObject.GetComponent<TimeManager>();
        distanceManager = GameObject.Find("DistanceManager").gameObject.GetComponent<DistanceManager>();
		spawnManager = GameObject.Find("SpawnManager").gameObject.GetComponent<AirEnemyManager>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //timeText.text = Mathf.RoundToInt(timeManager.flightDuration).ToString();
        timeText.text = timeManager.flightDuration.ToString("F2");
		bonusLvTimeText.text = spawnManager.showTimeinUI.ToString("F2");
        distanceText.text = Mathf.RoundToInt(distanceManager.maxDistance).ToString() + "  meters";
        scoreUIElement.GetComponent<Text>().text = DataManager.Instance.playerScore.ToString();
        CheckGameOver();
	}

    void CheckGameOver()
    {
        if (DataManager.Instance.gameOver)
        {
            StartCoroutine(ShowGameOverText());
        }
    }

    IEnumerator ShowGameOverText()
    {
        yield return new WaitForSeconds(3);
        gameOverImage.SetActive(true);
        //timeManager.pauseGame = true;
    }

    public void RestartSurvivalGame()
    {
        ServerTalk.Instance.UpdatePlayerScores();
        DataManager.Instance.playerScore = 0;
        SceneMainManager.Instance.RestartGame("Main");
    }

    public void LoadPlaneSelectScene()
    {
        ServerTalk.Instance.UpdatePlayerScores();
        DataManager.Instance.playerScore = 0;
        SceneMainManager.Instance.RestartGame("PlaneSelect");
    }

    public void DisableSound()
    {
        if (GameManager.Instance.soundOn)
        {
            Camera.main.GetComponent<AudioListener>().enabled = false;
            GameManager.Instance.soundOn = false;
            soundOffImage.SetActive(true);
        }
        else
        {
            Camera.main.GetComponent<AudioListener>().enabled = true;
            GameManager.Instance.soundOn = true;
            soundOffImage.SetActive(false);
        }

    }

    public void MuteSound()
    {
        isMute = !isMute;
        AudioListener.volume = isMute ? 0 : 1;
    }

}
