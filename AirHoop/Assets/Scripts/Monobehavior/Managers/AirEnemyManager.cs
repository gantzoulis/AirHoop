using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AirEnemyManager : MonoBehaviour
{
    [System.Serializable]
    public class AirLevel
    {
        public string LevelName;
        public List<GameObject> airEnemies;
        public float distanceToSpawn;
        public float timeLimitPerSpawn;
        public float levelLength;
    }

    [System.Serializable]
    public class AirLevelBonus
    {
        public string LevelName;
        public List<GameObject> bonusLevelPrefabs;
        public float bonusLevelTime;
        public float offSet;
    }

    [System.Serializable]
    public class AirSpawnLevel
    {
        public bool isBonusLevel;
        public AirLevel airLevel;
        public AirLevelBonus airLevelBonus;
    }

    private GameObject airSpawnPoint;
    private GameObject airSpawnPointToReach;
    [SerializeField] private float offSetSpawn = 40f;

    [SerializeField] float airEnemySpawnPointY_min;
    [SerializeField] float airEnemySpawnPointY_max;

    [SerializeField] private List<AirSpawnLevel> airSpawnLevelList;
    private int airSpawnLevelListIndex = 0;

    private GameObject player;

    private float timeToSpawn = 0f;
    private bool timeReset = true;
    public float showTimeinUI = 0;
    private float offsetTime;
    private GameObject tempAirBonusStage;

    private GameObject airSpawnBonusLv;

    public delegate void OnAirEnemyLevelCreation(List<GameObject> newAirEnemyList);
    public static event OnAirEnemyLevelCreation onAirEnemyLevelCreation;
    private int currentLevelIntex = -1;

    public GameObject specialLVTimeLabel;
    public GameObject specialLVTimeText;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        airSpawnPoint = new GameObject("Air Enemy Spawn Point");
        airSpawnPoint.transform.SetParent(player.transform);
        airSpawnPoint.transform.Translate(offSetSpawn, 0, 0);

        airSpawnPointToReach = new GameObject("Air Enemy Spawn Point To Reach");
        airSpawnPointToReach.transform.Translate(airSpawnPoint.transform.position.x, 0, 0);
    }

    void Update()
    {
        SpawnAirEnemy();
    }

    private void SpawnAirEnemy()
    {
        if (currentLevelIntex != airSpawnLevelListIndex)
        {
            onAirEnemyLevelCreation(airSpawnLevelList[airSpawnLevelListIndex].airLevel.airEnemies);
            currentLevelIntex = airSpawnLevelListIndex;
        }

        switch (airSpawnLevelList[airSpawnLevelListIndex].isBonusLevel)
        {
            case false:
                AirLevel tempAirEnemyLevel = airSpawnLevelList[airSpawnLevelListIndex].airLevel;
                GameObject tempAirEnemy = tempAirEnemyLevel.airEnemies[Random.Range(0, tempAirEnemyLevel.airEnemies.Count)];

                if (timeReset)
                {
                    timeToSpawn = tempAirEnemyLevel.timeLimitPerSpawn;
                    timeReset = false;
                }

                if (timeToSpawn <= 0 || airSpawnPoint.transform.position.x >= airSpawnPointToReach.transform.position.x)
                {
                    Vector3 tempSpawnPoint = new Vector3(airSpawnPoint.transform.position.x, Random.Range(airEnemySpawnPointY_min, airEnemySpawnPointY_max), 0f);
                    
                    Pooling.Instance.InstantiateAirEnemy(tempAirEnemy, tempSpawnPoint);
                    timeReset = true;
                    float difference = airSpawnPointToReach.transform.position.x - tempSpawnPoint.x;
                    airSpawnPointToReach.transform.Translate(tempAirEnemyLevel.distanceToSpawn - difference, 0, 0);
                }

                timeToSpawn -= Time.deltaTime;
                if (DataManager.Instance.maxDistance >= tempAirEnemyLevel.levelLength)
                {
                    if (airSpawnLevelListIndex < airSpawnLevelList.Count - 1)
                    {
                        airSpawnLevelListIndex++;
                        timeReset = true;
                    }
                }
                break;
            case true:
                AirLevelBonus tempAirEnemyLevelBonus = airSpawnLevelList[airSpawnLevelListIndex].airLevelBonus;
                GameObject tempAirBonusStage = tempAirEnemyLevelBonus.bonusLevelPrefabs[Random.Range(0, tempAirEnemyLevelBonus.bonusLevelPrefabs.Count)];               

                offsetTime = (Vector3.Distance(player.transform.position, airSpawnPoint.transform.position) / player.GetComponent<Aircraft_motor>().aircraft.speed) - 3f;
               
                if (timeReset)
                {
                    timeToSpawn = tempAirEnemyLevelBonus.bonusLevelTime + Time.time + offsetTime;                  
                    timeReset = false;
                    airSpawnBonusLv = Instantiate(tempAirBonusStage,
                       new Vector3(airSpawnPoint.transform.position.x + airSpawnLevelList[airSpawnLevelListIndex].airLevelBonus.offSet, 0, 0), Quaternion.identity);
                    Debug.Log("Bonus spawned");

                    StartCoroutine(WaitToSpawnLvBonus(offsetTime));
                }

                if (timeToSpawn - Time.time <= 0)
                {
                    if (airSpawnLevelListIndex < airSpawnLevelList.Count - 1)
                    {
                        Destroy(airSpawnBonusLv);
                        timeReset = true;
                        airSpawnLevelListIndex++;
                        specialLVTimeLabel.SetActive(false);
                    }
                }

                showTimeinUI = timeToSpawn - Time.time;
                break;
            default:
                Debug.LogWarning("AirSpawnManager: Do not know what to spawn!!!");
                return;
        }
    }

    private IEnumerator WaitToSpawnLvBonus(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        specialLVTimeLabel.SetActive(true);
    }
}
