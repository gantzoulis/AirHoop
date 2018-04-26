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

    public PlayerDataClass playerData = new PlayerDataClass();




    /*****************************************
     * IEnumerators
     *****************************************/
    IEnumerator _GetPlayerData(string _userID)
    {
        Debug.Log("Coroutine started " + _userID);
        string getUrl = serverURL;
        Debug.Log("GetURL is " + getUrl);

        WWWForm authForm = new WWWForm();
        authForm.AddField("username", _userID);
        authForm.AddField("action", "authorize");

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
            /*
            if (jsnData == "0x000")
            {
                string errorReportingMessage = "Player authentication Error - 0x000";
                Debug.Log(errorReportingMessage);
                //ShowErrorMessage(errorReportingMessage);
            }
            else
            {
                playerData = JsonUtility.FromJson<PlayerDataClass>(jsnData);
                StartCoroutine(_GetPlayerStats(playerData.playerHash));
            }
            */
        }
    }

    public void GetPlayerData(string userid)
    {
        StartCoroutine(_GetPlayerData(userid));
    }

}
