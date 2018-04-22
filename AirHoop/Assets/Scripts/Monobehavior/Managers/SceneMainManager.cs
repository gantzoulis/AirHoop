using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMainManager : MonoBehaviour
{
    #region Singleton Definition
    private static SceneMainManager instance;
    public static SceneMainManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<SceneMainManager>();
            }
            return instance;
        }
    }
    #endregion
    public void LoadGameScene(string gameScene)
    {
        SceneManager.LoadScene(gameScene);
    }

    public void RestartGame(string gameScene)
    {
        DataManager.Instance.gameOver = false;
        SceneManager.LoadScene(gameScene);
    }
}
