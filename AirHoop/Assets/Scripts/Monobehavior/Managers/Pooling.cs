using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling : MonoBehaviour
{
    private static Pooling _instance;
    public static Pooling Instance
    {
        get { return _instance; }
    }

    private GameObject enemyAirPool;
    private List<GameObject> enemyPoolListPrefabs = new List<GameObject>();
    [SerializeField] private int enemyGameObjectMultiplier = 5;

    private GameObject buffPool;
    private List<GameObject> buffPoolListPrefabs = new List<GameObject>();
    [SerializeField] private int buffGameObjectMultiplier = 5;
    
    private GameObject groundPool;
    [SerializeField] private List<GameObject> groundPoolListPrefabs = new List<GameObject>();
    [SerializeField] private int groundGameObjectMultiplier = 5;

    private GameObject backgroundPool;
    [SerializeField] private List<GameObject> backgroundPoolListPrefabs = new List<GameObject>();
    [SerializeField] private int backgroundGameObjectMultiplier = 5;

    private GameObject frontgroundPool;
    private List<GameObject> frontgroundPoolListPrefabs = new List<GameObject>();
    [SerializeField] private int frontgroundGameObjectMultiplier = 5;

    void Awake()
    {
        Singleton();

        enemyAirPool = new GameObject("Air Enemy Pool");
        enemyAirPool.transform.parent = this.gameObject.transform;

        buffPool = new GameObject("Buff Pool");
        buffPool.transform.parent = this.gameObject.transform;

        groundPool = new GameObject("Ground Pool");
        groundPool.transform.parent = this.gameObject.transform;

        backgroundPool = new GameObject("Background Pool");
        backgroundPool.transform.parent = this.gameObject.transform;

        frontgroundPool = new GameObject("Frontground Pool");
        frontgroundPool.transform.parent = this.gameObject.transform;
    }

    void OnEnable()
    {
        AirEnemyManager.onAirEnemyLevelCreation += GetAirEnemyList;
        AirEnemyManager.buffLevelCreation += GetBuffList;
        GroundManager.onLevelCreation += GetGroundList;
        BackGroundManager.onLevelCreationBackground += GetBackgroundList;
        BackGroundManager.onLevelCreationFrontground += GetFrontgroundList;
    }

    void OnDisable()
    {
        AirEnemyManager.onAirEnemyLevelCreation -= GetAirEnemyList;
        AirEnemyManager.buffLevelCreation -= GetBuffList;
        GroundManager.onLevelCreation -= GetGroundList;
        BackGroundManager.onLevelCreationBackground -= GetBackgroundList;
        BackGroundManager.onLevelCreationFrontground -= GetFrontgroundList;
    }

    void Start()
    {
    
    }

    private void Singleton()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void GetAirEnemyList(List<GameObject> newAirEnemyList)
    {
        enemyPoolListPrefabs.Clear();

        foreach (Transform child in enemyAirPool.transform)
        {
            if (!child.gameObject.activeSelf)
            {
               Destroy(child.gameObject);
            }
        }

        foreach (GameObject go in newAirEnemyList)
        {
            enemyPoolListPrefabs.Add(go);
        }

        foreach (GameObject go in enemyPoolListPrefabs)
        {
            for (int i = 0; i < enemyGameObjectMultiplier; i++)
            {
                GameObject enemy = Instantiate(go, Vector3.zero, Quaternion.identity, enemyAirPool.transform);
                enemy.SetActive(false);
            }
        }
    }

    public void InstantiateAirEnemy(GameObject go, Vector3 spawnPoint)
    {
        foreach (Transform child in enemyAirPool.transform)
        {
            GameObject childGO = child.gameObject;
            if (childGO.name.Contains(go.name) && !childGO.activeSelf)
            {
                childGO.SetActive(true);
                childGO.transform.position = spawnPoint;
                return;
            }
        }

        GameObject newGO = Instantiate(go, spawnPoint, Quaternion.identity);
        newGO.transform.parent = enemyAirPool.transform;
    }

    private void GetBuffList(List<GameObject> newBuffList)
    {
        buffPoolListPrefabs.Clear();

        foreach (Transform child in buffPool.transform)
        {
            if (!child.gameObject.activeSelf)
            {
                Destroy(child.gameObject);
            }
        }

        foreach (GameObject go in newBuffList)
        {
            buffPoolListPrefabs.Add(go);
        }

        foreach (GameObject go in buffPoolListPrefabs)
        {
            for (int i = 0; i < buffGameObjectMultiplier; i++)
            {
                GameObject buff = Instantiate(go, Vector3.zero, Quaternion.identity, buffPool.transform);
                buff.SetActive(false);
            }
        }
    }

    public void InstantiateBuff(GameObject go, Vector3 spawnPoint)
    {
        foreach (Transform child in buffPool.transform)
        {
            GameObject childGO = child.gameObject;
            if (childGO.name.Contains(go.name) && !childGO.activeSelf)
            {
                childGO.SetActive(true);
                childGO.transform.position = spawnPoint;
                return;
            }
        }

        GameObject newGO = Instantiate(go, spawnPoint, Quaternion.identity);
        newGO.transform.parent = buffPool.transform;
    }

    private void GetGroundList(List<GameObject> newGroundList)
    {
        groundPoolListPrefabs.Clear();

        foreach (Transform child in groundPool.transform)
        {
            if (!child.gameObject.activeSelf)
            {
                Destroy(child.gameObject);
            }
        }

        foreach (GameObject go in newGroundList)
        {
            groundPoolListPrefabs.Add(go);
        }

        foreach (GameObject go in groundPoolListPrefabs)
        {
            for (int i = 0; i < groundGameObjectMultiplier; i++)
            {
                GameObject ground = Instantiate(go, Vector3.zero, Quaternion.identity, groundPool.transform);
                ground.SetActive(false);
            }
        }
    }

    public void InstantiateGround(GameObject go, Vector3 spawnPoint)
    {
        foreach (Transform child in groundPool.transform)
        {
            GameObject childGO = child.gameObject;
            if (childGO.name.Contains(go.name) && !childGO.activeSelf)
            {
                childGO.SetActive(true);
                childGO.transform.position = spawnPoint;
                return;
            }
        }

        GameObject newGO = Instantiate(go, spawnPoint, Quaternion.identity);
        newGO.transform.parent = groundPool.transform;
    }

    private void GetBackgroundList(List<GameObject> newBackgroundList)
    {
        backgroundPoolListPrefabs.Clear();

        foreach (Transform child in backgroundPool.transform)
        {
            if (!child.gameObject.activeSelf)
            {
                Destroy(child.gameObject);
            }
        }

        foreach (GameObject go in newBackgroundList)
        {
            backgroundPoolListPrefabs.Add(go);
        }

        foreach (GameObject go in backgroundPoolListPrefabs)
        {
            for (int i = 0; i < backgroundGameObjectMultiplier; i++)
            {
                GameObject background = Instantiate(go, Vector3.zero, Quaternion.identity, backgroundPool.transform);
                background.SetActive(false);
            }
        }
    }

    public void InstantiateBackground(GameObject go, Vector3 spawnPoint)
    {
        foreach (Transform child in backgroundPool.transform)
        {
            GameObject childGO = child.gameObject;
            if (childGO.name.Contains(go.name) && !childGO.activeSelf)
            {
                childGO.SetActive(true);
                childGO.transform.position = spawnPoint;
                return;
            }
        }

        GameObject newGO = Instantiate(go, spawnPoint, Quaternion.identity);
        newGO.transform.parent = backgroundPool.transform;
    }

    private void GetFrontgroundList(List<GameObject> newFrontgroundList)
    {
        frontgroundPoolListPrefabs.Clear();

        foreach (Transform child in frontgroundPool.transform)
        {
            if (!child.gameObject.activeSelf)
            {
                Destroy(child.gameObject);
            }
        }

        foreach (GameObject go in newFrontgroundList)
        {
            frontgroundPoolListPrefabs.Add(go);
        }

        foreach (GameObject go in frontgroundPoolListPrefabs)
        {
            for (int i = 0; i < frontgroundGameObjectMultiplier; i++)
            {
                GameObject frontground = Instantiate(go, Vector3.zero, Quaternion.identity, frontgroundPool.transform);
                frontground.SetActive(false);
            }
        }
    }

    public void InstantiateFrontground(GameObject go, Vector3 spawnPoint)
    {
        foreach (Transform child in frontgroundPool.transform)
        {
            GameObject childGO = child.gameObject;
            if (childGO.name.Contains(go.name) && !childGO.activeSelf)
            {
                childGO.SetActive(true);
                childGO.transform.position = spawnPoint;
                return;
            }
        }

        GameObject newGO = Instantiate(go, spawnPoint, Quaternion.identity);
        newGO.transform.parent = frontgroundPool.transform;
    }
}
