using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    #region Player Related
        [SerializeField]
        private GameObject player;
        private GameObject playerSpawnObject;
        [SerializeField]
        private Transform playerSpawnPos;
    #endregion

    private void Awake()
    {
        InitializeLevel();
        SpawnPlayer();
    }

    // Use this for initialization
    void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        CalculateTimeFuel();
    }

    private void InitializeLevel()
    {
        DataManager.Instance.defaultPlayerSpawnPos = playerSpawnPos.position;
    }

    private void SpawnPlayer()
    {
        playerSpawnObject = DataManager.Instance.choosenAircraft.model[0];
        player = Instantiate(playerSpawnObject, playerSpawnPos.position, Quaternion.identity);
        Camera.main.GetComponent<MultipleTargetCamera>().targets.Add(player.transform);
        DataManager.Instance.playerObject = player;
    }

    public void RestartGame()
    {
        DataManager.Instance.gameOver = false;
        SceneManager.LoadScene("Main");
    }

    public IEnumerator RespawnPlayer(Vector3 spawnPosition, Quaternion spawnRotation)
    {
        DataManager.Instance.playerObject.SetActive(false);
        DataManager.Instance.playerIsActive = true;
        DataManager.Instance.playerLives--;
        DataManager.Instance.playerObject.transform.position = spawnPosition;
        //DataManager.Instance.playerObject.transform.rotation = playerDeathRotation;

        yield return new WaitForSeconds(2.5f);
        DataManager.Instance.playerObject.SetActive(true);
        StartCoroutine(DataManager.Instance.playerObject.GetComponent<Aircraft_motor>().ExtraLifeFlasher());
    }

    private void CalculateTimeFuel()
    {
       DataManager.Instance.playerTimeLapseFuel += DataManager.Instance.timeLapseRatio * Time.deltaTime;
    }
}
