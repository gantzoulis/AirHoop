using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class ServerTalk : MonoBehaviour
{
    #region Singleton Definition
    private static ServerTalk instance;
    public static ServerTalk Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<ServerTalk>();
            }
            return instance;
        }
    }
    #endregion

    [SerializeField]
    private string serverURL;

    [SerializeField]
    private string serverScoresURL;
    /*
    [Header("Player Information - From server")]
    public PlayerDataClass playerData = new PlayerDataClass();
    */
    /*****************************************
     * IEnumerators
     *****************************************/
    IEnumerator _GetPlayerData(string _userID)
    {
        //Debug.Log("Coroutine started " + _userID);
        string getUrl = serverURL;
        //Debug.Log("GetURL is " + getUrl);

        WWWForm authForm = new WWWForm();
        authForm.AddField("php_userID", _userID);
        authForm.AddField("php_action", "authorize");

        UnityWebRequest www = UnityWebRequest.Post(getUrl, authForm);

        //yield return www.Send()
        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
            string errorReportingMessage = "Oops. Something went wrong. (error 0x000-Connection Error)";
            Debug.Log(errorReportingMessage);
            //ShowErrorMessage(errorReportingMessage);
        }
        else
        {
            string jsnData = www.downloadHandler.text;
            Debug.Log(jsnData);
            //ServerManager.Instance.BuildPlayerData(jsnData);
            ServerManager.Instance.playerData = JsonUtility.FromJson<PlayerDataClass>(jsnData);
            //playerData = JsonUtility.FromJson<PlayerDataClass>(jsnData);
            Debug.Log(ServerManager.Instance.playerData.player_nation);
        }
    }


    IEnumerator _UpdatePlayerScores(string _userID)
    {
        //Debug.Log("Coroutine started " + _userID);
        string getUrl = serverScoresURL;
        //Debug.Log("GetURL is " + getUrl);

        WWWForm authForm = new WWWForm();
        authForm.AddField("php_userID", _userID);
        authForm.AddField("php_action", "authorize");

        UnityWebRequest www = UnityWebRequest.Post(getUrl, authForm);

        //yield return www.Send()
        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
            string errorReportingMessage = "Oops. Something went wrong. (error 0x000-Connection Error)";
            Debug.Log(errorReportingMessage);
            //ShowErrorMessage(errorReportingMessage);
        }
        else
        {
            string jsnData = www.downloadHandler.text;
            Debug.Log(jsnData);
            //ServerManager.Instance.BuildPlayerData(jsnData);
            ServerManager.Instance.playerData = JsonUtility.FromJson<PlayerDataClass>(jsnData);
            //playerData = JsonUtility.FromJson<PlayerDataClass>(jsnData);
            Debug.Log(ServerManager.Instance.playerData.player_nation);
        }
    }

    /*****************************************
     * Server Functions
     *****************************************/

    public void GetPlayerData(string _userID)
    {
        Debug.Log("Connecting to server for " + _userID);
        StartCoroutine(_GetPlayerData(_userID));
    }

    public void UpdatePlayerScores()
    {
        Debug.Log("Updating Players Scores");
    } 
}
