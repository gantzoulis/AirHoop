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
    [SerializeField] private List<GameObject> enemyPoolListPrefabs;
    [SerializeField] private int enemyGameObjectMultiplier = 5;

    
    private GameObject groundPool;
    [SerializeField] private List<GameObject> groundPoolListPrefabs;
    [SerializeField] private int groundGameObjectMultiplier = 5;

    void Awake()
    {
        Singleton();
    }

    void OnEnable()
    {
        AirEnemyManager.onAirEnemyLevelCreation += GetAirEnemyList;
    }

    void OnDisable()
    {
        AirEnemyManager.onAirEnemyLevelCreation -= GetAirEnemyList;
    }

    void Start()
    {
        enemyAirPool = new GameObject("Air Enemy Pool");
        enemyAirPool.transform.parent = this.gameObject.transform;

        groundPool = new GameObject("Ground Pool");
        groundPool.transform.parent = this.gameObject.transform;
        foreach (GameObject go in groundPoolListPrefabs)
        {
            for (int i = 0; i < groundGameObjectMultiplier; i++)
            {
                GameObject ground = Instantiate(go, Vector3.zero, Quaternion.identity, groundPool.transform);
                ground.SetActive(false);
            }
        }

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
                GameObject.Destroy(child.gameObject);
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
}
