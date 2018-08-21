using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoreOptionsButton : MonoBehaviour
{

    [SerializeField]
    private GameObject leaderBoardBtn;
    [SerializeField]
    private GameObject achievementsBtn;
    [SerializeField]
    private GameObject settingsBtn;

    [SerializeField]
    private GameObject leaderBoardPanel;
    [SerializeField]
    private GameObject scoresGrid;

//    [SerializeField]
    private bool leaderPnlActive = false;

    private bool scoresCleared = false;
    private bool moreSettingsPanel = false;
    

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        OpenLeaderBoardPanel();
    }

    public void ToggleLeaderBoardPanel()
    {
        leaderPnlActive = !leaderPnlActive;
    }

    private void OpenLeaderBoardPanel()
    {
        if (leaderPnlActive)
        {
            leaderBoardPanel.SetActive(true);
            if (!scoresCleared)
            {
                ClearLeaderBoard();
            }
            
        }
        else
        {
            leaderBoardPanel.SetActive(false);
            scoresCleared = false;
        }
    }

    private void ClearLeaderBoard()
    {
        foreach (Transform t in scoresGrid.transform)
        {
            Destroy(t.gameObject);
        }
        scoresCleared = true;
    }

    public void ToggleMoreSettingsButton()
    {
        if (!moreSettingsPanel)
        {
            leaderBoardBtn.GetComponent<Animator>().SetTrigger("PlayNormal");
            settingsBtn.GetComponent<Animator>().SetTrigger("PlayNormal");
            achievementsBtn.GetComponent<Animator>().SetTrigger("PlayNormal");
            moreSettingsPanel = true;
        }
        else
        {
            leaderBoardBtn.GetComponent<Animator>().SetTrigger("PlayReverse");
            settingsBtn.GetComponent<Animator>().SetTrigger("PlayReverse");
            achievementsBtn.GetComponent<Animator>().SetTrigger("PlayReverse");
            moreSettingsPanel = false;
        }
    }
}
