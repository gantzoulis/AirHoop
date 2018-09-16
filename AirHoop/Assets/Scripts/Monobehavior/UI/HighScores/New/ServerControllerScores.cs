using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ServerControllerScores : MonoBehaviour
{

    public ScoresList ScoresList = new ScoresList();
    [SerializeField]
    private string scoreListURL;
    

    [SerializeField]
    private GameObject scoresGrid;
    
    [SerializeField]
    private GameObject playerScoreEntryPrefab;
    [SerializeField]
    private GameObject descriptionTextPrefab;
    [SerializeField]
    private GameObject playernameTextPrefab;

    public enum ldrBoardType {ACESHIGH,REDBARON, EARHART, FOGG }
    public ldrBoardType ldrBoardTypeEnum;
    [SerializeField]
    private string leaderBoardType = ldrBoardType.ACESHIGH.ToString();


    private void OnEnable()
    {
        Debug.Log("High Scores Panel is Enabled");
        GetLeaderBoard(leaderBoardType);
    }

    private void GetLeaderBoard(string leaderBrdType)
    {
        StartCoroutine(_GetPlayerScores(leaderBrdType));
    }

    private string fixJson(string value)
    {
        value = "{\"Scores\":" + value + "}";
        return value;
    }

    IEnumerator _GetPlayerScores(string leaderBoardType)
    {
        string getUrl = scoreListURL;
        Debug.Log("Getting LeaderBoard: " + leaderBoardType);
        WWWForm leaderForm = new WWWForm();
        //options should be 
        //ACESHIGH - Max Score
        //REDBARON - Max Coins
        //EARHART - Max Distance
        //FOGG - Total Distance
        //
        //REGIONAL
        //WORLD
        leaderForm.AddField("php_leaderBoardType", leaderBoardType);
        leaderForm.AddField("php_userID", DataManager.Instance.playerName); 
        UnityWebRequest www = UnityWebRequest.Post(getUrl, leaderForm);

        //yield return www.Send()
        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
            string errorReportingMessage = "Oops. Something went wrong. (error 0x000-Connection Error)";
            Debug.Log(errorReportingMessage);
        }
        else
        {
            string jsnData = www.downloadHandler.text;
            Debug.Log("Server Reply: " + jsnData);
            string fixedJsn = fixJson(jsnData);
            ScoresList = JsonUtility.FromJson<ScoresList>(jsnData);

            switch (leaderBoardType)
            {
                case "ACESHIGH":
                    foreach (var item in ScoresList.Scores)
                    {
                        InstantiateScore(item.userid, item.player_high_score);
                    }
                    break;
                case "REDBARON":
                    break;
                case "EARHART":
                    break;
                case "FOGG":
                    break;
                default:
                    break;
            }

            
                
            
            
            
        }
    }

    private void InstantiateScore(string _playerName, int _playerScore)
    {
        GameObject playerScore = Instantiate(playerScoreEntryPrefab) as GameObject;
        playerScore.transform.SetParent(scoresGrid.transform, false);
        playerScore.transform.Find(playernameTextPrefab.name).GetComponent<Text>().text = _playerName;
        playerScore.transform.Find(descriptionTextPrefab.name).GetComponent<Text>().text = _playerScore.ToString();
    }

    /*
    private void InstantiateScore_OLD(string _playername, int _rank, float _score)
    {
        GameObject scoreRank = Instantiate(rankTextPrefab) as GameObject;
        GameObject playerName = Instantiate(playerTextPrefab) as GameObject;
        GameObject playerScore = Instantiate(playerScoreEntryPrefab) as GameObject;
        scoreRank.transform.SetParent(GameObject.Find("ScoresList").transform, false);
        playerName.transform.SetParent(GameObject.Find("ScoresList").transform, false);
        playerScore.transform.SetParent(scoresGrid.transform, false);
        scoreRank.GetComponent<Text>().text = _rank.ToString();
        scoreRank.transform.position = new Vector2(scoreRank.transform.position.x, scoreRank.transform.position.y - (40 * _rank));
        playerName.GetComponent<Text>().text = _playername;
        playerName.transform.position = new Vector2(playerName.transform.position.x, playerName.transform.position.y - (40 * _rank));
        playerScore.GetComponent<Text>().text = SurvivalScore(_score);
        playerScore.transform.position = new Vector2(playerScore.transform.position.x, playerScore.transform.position.y - (40 * _rank));
    }
    */
}
