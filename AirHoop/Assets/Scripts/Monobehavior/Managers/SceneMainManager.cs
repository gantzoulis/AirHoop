using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMainManager : MonoBehaviour
{
    public void LoadGameScene(string gameScene)
    {
        SceneManager.LoadScene(gameScene);
    }
}
