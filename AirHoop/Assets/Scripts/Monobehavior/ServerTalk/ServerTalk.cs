using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;
using System;

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


    [Header("Server Connections")]
    [SerializeField]
    private string serverURL;

    [SerializeField]
    private string serverScoresURL;

    [SerializeField]
    private string planeCostsURL;
    [SerializeField]
    private string planeOwnURL;
    [SerializeField]
    private string purchagePlaneURL;

    [Header("UI Related")]
    public GameObject srvMesBoxObj;
    [SerializeField]
    private GameObject loginUIObj;
    public Text serverMessageBox;
    public bool srvMesBoxToggle = false;

    

    /*****************************
     * 
     *  Unity related
     *
     ****************************/

    void Update()
    {
        //Debug.Log("Server Talk");
    }

    /*****************************************
     * IEnumerators
     *****************************************/
    IEnumerator _GetPlayerData(string _userID)
    {
        Debug.Log("[ServerTalk] _GetPlayerData for user: " + _userID);
        string getUrl = serverURL;

        WWWForm authForm = new WWWForm();
        authForm.AddField("php_userID", _userID);
        authForm.AddField("php_action", "authorize");

        UnityWebRequest www = UnityWebRequest.Post(getUrl, authForm);
        srvMesBoxObj.SetActive(true);
        loginUIObj.SetActive(false);
        serverMessageBox.text = "Connecting To Server";
        //yield return www.Send()
        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
            //srvMesBoxObj.SetActive(true);
            string errorReportingMessage = "Oops. Something went wrong. (error 0x000-Connection Error)";
            serverMessageBox.text = errorReportingMessage;
            Debug.Log(errorReportingMessage);
            loginUIObj.SetActive(true);
            //ShowErrorMessage(errorReportingMessage);
        }
        else
        {
            string jsnData = www.downloadHandler.text;
            //Debug.Log(jsnData);
            string successReportingMessage = "User is Logged On! Welcome Pilot";
            serverMessageBox.text = successReportingMessage;
            StartCoroutine(_CloseMessageBox());
            ServerManager.Instance._Get_PlayerData();
            ServerManager.Instance.playerData = JsonUtility.FromJson<PlayerDataClass>(jsnData);
        }
    }

    IEnumerator _CloseMessageBox()
    {
        yield return new WaitForSeconds(3);
        srvMesBoxObj.SetActive(false);
        //loginUIObj.SetActive(false);
    }

    IEnumerator _UpdatePlayerScores(string _userID)
    {
        string getUrl = serverScoresURL;
        
        WWWForm updUserForm = new WWWForm();
        updUserForm.AddField("php_userID", _userID);
        updUserForm.AddField("php_action", "update");
        updUserForm.AddField("php_distance", DataManager.Instance.maxDistance.ToString());
        updUserForm.AddField("php_score", DataManager.Instance.playerFinalScore);
        updUserForm.AddField("php_player_triodinium", DataManager.Instance.playerEarnedTriodinium);
        updUserForm.AddField("php_playerCoins", DataManager.Instance.playerCoins);
        updUserForm.AddField("php_playerTime", DataManager.Instance.playerFlightTime.ToString());

        UnityWebRequest www = UnityWebRequest.Post(getUrl, updUserForm);

        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
            string errorReportingMessage = "Oops. Something went wrong. (error 0x000-Connection Error)";
            Debug.Log(errorReportingMessage);
        }
        else
        {
            string serverData = www.downloadHandler.text;
            Debug.Log("Player Was updated"+ serverData);
            ServerManager.Instance.playerData = JsonUtility.FromJson<PlayerDataClass>(serverData);
        }
    }

    IEnumerator _GetAirplaneCost(string _planeID, int _elementID)
    {
        string getUrl = planeCostsURL;

        WWWForm updPlaneForm = new WWWForm();
        updPlaneForm.AddField("php_planeID", _planeID);
        
        UnityWebRequest www = UnityWebRequest.Post(getUrl, updPlaneForm);

        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
            string errorReportingMessage = "Oops. Something went wrong. (error 0x000-Connection Error)";
            Debug.Log(errorReportingMessage);
        }
        else
        {
            string serverData = www.downloadHandler.text;
            int currentPlaneCost = Convert.ToInt32(serverData);
            DataManager.Instance.airplaneList[_elementID].airPlaneCost = currentPlaneCost;
        }
    }

    IEnumerator _GetAirplaneOwn(string _userID, string _planeID, int _elementID)
    {
        string getUrl = planeOwnURL;
        //Debug.Log("GetURL is " + getUrl + "user: " + _userID + "plane: " + _planeID);

        WWWForm updPlaneForm = new WWWForm();
        updPlaneForm.AddField("php_userID", _userID);
        updPlaneForm.AddField("php_planeID", _planeID);

        UnityWebRequest www = UnityWebRequest.Post(getUrl, updPlaneForm);

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
            bool isOwned = false;
            string serverData = www.downloadHandler.text;
            //Debug.Log("PHP Says: " + serverData);
            int currentPlaneOwn = Convert.ToInt32(serverData);
            if (currentPlaneOwn == 1)
            {
                isOwned = true;
            }
            DataManager.Instance.airplaneList[_elementID].playerOwned = isOwned;
            //Debug.Log("Converted Value: " + currentPlaneCost);
        }
    }

    IEnumerator _PurchagePlane(string _userID, string _planeID)
    {
        string getUrl = purchagePlaneURL;
        Debug.Log("GetURL is " + getUrl + "user: " + _userID + "plane: " + _planeID);

        WWWForm updPlaneForm = new WWWForm();
        updPlaneForm.AddField("php_userID", _userID);
        updPlaneForm.AddField("php_planeID", _planeID);

        UnityWebRequest www = UnityWebRequest.Post(getUrl, updPlaneForm);

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
            string serverData = www.downloadHandler.text;
            if (serverData == "ACK_1")
            {
                Debug.Log("Transaction is completed");
                ServerManager.Instance.RebuildPlaneSelection(_userID);
            }
            Debug.Log("PHP Says: " + serverData);
            AudioSource buyButtonAudio = GameObject.Find("Buy").GetComponent<AudioSource>();
            buyButtonAudio.Play();
        }
    }
    /*****************************************
     * Server Functions
     *****************************************/


    public void PurchagePlane(string _userID, string _planeID)
    {
        Debug.Log("Starting Purchage Plane function for " + _userID + " buying " + _planeID);
        StartCoroutine(_PurchagePlane(_userID, _planeID));
    }

    public void GetPlayerData(string _userID)
    {
        Debug.Log("[ServerTalk] Connecting to server for " + _userID);
        StartCoroutine(_GetPlayerData(_userID));
        ServerManager.Instance.BuildPlaneSelection(_userID);
    }

    public void UpdatePlayerScores()
    {
        string userID = DataManager.Instance.playerName;
        Debug.Log("Updating Players Scores for "+ userID);
        StartCoroutine(_UpdatePlayerScores(userID));
    }

    public void GetPlaneCost(string planeID,int elementID)
    {
        StartCoroutine(_GetAirplaneCost(planeID, elementID));
    }

    public void GetPlaneOwn(string userID, string planeID, int elementID)
    {
        Debug.Log("[ServerTalk] Getting ownership for " + userID);
        StartCoroutine(_GetAirplaneOwn(userID, planeID, elementID));
    }
    /*
    public void returnPlaneCost(int cost)
    {
        Debug.Log("Getting cost for:  Cost: " + currentPlaneCost);
    }
    */
}
