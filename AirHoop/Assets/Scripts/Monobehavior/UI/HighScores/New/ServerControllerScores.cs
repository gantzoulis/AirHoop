using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerControllerScores : MonoBehaviour
{

    public ScoresList ScoresList = new ScoresList();
    [SerializeField]
    private string scoreListURL;



    private void OnEnable()
    {
        Debug.Log("High Scores Panel is Enabled");
    }


    
}
