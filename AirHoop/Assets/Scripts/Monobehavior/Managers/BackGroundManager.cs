using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BackGroundManager : MonoBehaviour
{
    private GameObject backgroundSpawnPoint;
    private GameObject backgroundSpawnPointToReach;
    [SerializeField] private float offSetSpawn = 60f;

    private GameObject player;

    [SerializeField] private List<GameObject> backgroundPrefab;
    private float backgroundPrefabSizeX;

    [SerializeField] private float firstSpawnPointX = 40f;
    private float nextSpawnPointX;

    public delegate void OnLevelCreation(List<GameObject> newBackgroundList);
    public static event OnLevelCreation onLevelCreationBackground;
    public static event OnLevelCreation onLevelCreationFrontground;

    [System.Serializable]
    public class FrontGroundItems
    {
        public List<GameObject> itemsPrefab;
        public float spawnDistance;
        public float varDistance;
        public bool spawnOnBase;
    }

    [SerializeField] private List<FrontGroundItems> frontgroundItems = new List<FrontGroundItems>();
    private List<GameObject> frontgroundPrefab = new List<GameObject>();

    [SerializeField] private float frontgroundSpawnPoint_Y_MIN;
    [SerializeField] private float frontgroundSpawnPoint_Y_MAX;

    private List<GameObject> frontGroundSpawnPointList;
    private List<GameObject> frontGroundSpawnPointListToReach;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        onLevelCreationBackground(backgroundPrefab);

        backgroundSpawnPoint = new GameObject("Background Spawn Point");
        backgroundSpawnPoint.transform.SetParent(player.transform);
        backgroundSpawnPoint.transform.Translate(offSetSpawn, 0, 0);

        backgroundSpawnPointToReach = new GameObject("Background Spawn Point To Reach");
        backgroundSpawnPointToReach.transform.Translate(backgroundSpawnPoint.transform.position.x, 0, 0);

        nextSpawnPointX = firstSpawnPointX;



        int indexFrontgroundItems = 1;
        frontGroundSpawnPointList = new List<GameObject>();
        frontGroundSpawnPointListToReach = new List<GameObject>();

        foreach (FrontGroundItems item in frontgroundItems)
        {
            foreach (GameObject go in item.itemsPrefab)
            {
                frontgroundPrefab.Add(go);
            }

            GameObject frontgroundSpawnPoint = new GameObject("Frontground Spawn Point " + indexFrontgroundItems);
            frontgroundSpawnPoint.transform.SetParent(player.transform);
            frontgroundSpawnPoint.transform.Translate(offSetSpawn, 0, 0);
            frontGroundSpawnPointList.Add(frontgroundSpawnPoint);

            GameObject frontgroundSpawnPointToReach = new GameObject("Frontground Spawn Point To Reach " + indexFrontgroundItems);
            frontgroundSpawnPointToReach.transform.Translate(frontgroundSpawnPoint.transform.position.x, 0, 0);
            frontGroundSpawnPointListToReach.Add(frontgroundSpawnPointToReach);

            indexFrontgroundItems++;
        }

        onLevelCreationFrontground(frontgroundPrefab);
    }

    void Update()
    {
        if (!DataManager.Instance.gameOver)
        {
            SpawnBackground();
            SpawnFrontground();
        }
    }

    private void SpawnBackground()
    {
        GameObject tempBackground = backgroundPrefab[Random.Range(0, backgroundPrefab.Count)];

        backgroundPrefabSizeX = tempBackground.GetComponent<BoxCollider>().size.x;

        if (backgroundSpawnPoint.transform.position.x >= backgroundSpawnPointToReach.transform.position.x)
        {
            Vector3 tempSpawnPoint = new Vector3(nextSpawnPointX, -13f, 10f);

            Pooling.Instance.InstantiateBackground(tempBackground, tempSpawnPoint);
        
            backgroundSpawnPointToReach.transform.Translate(Random.Range(backgroundPrefabSizeX/2, backgroundPrefabSizeX), 0, 0);

            nextSpawnPointX += backgroundPrefabSizeX;
        }
    }

    private void SpawnFrontground()
    {
            for (int i = 0; i < frontgroundItems.Count; i++)
            {
                GameObject tempFrontground = frontgroundItems[i].itemsPrefab[Random.Range(0, frontgroundItems[i].itemsPrefab.Count)];


                if (frontGroundSpawnPointList[i].transform.position.x >= frontGroundSpawnPointListToReach[i].transform.position.x)
                {
                    Vector3 tempSpawnPoint = Vector3.zero;

                    if (frontgroundItems[i].spawnOnBase)
                    {
                        tempSpawnPoint = new Vector3(frontGroundSpawnPointList[i].transform.position.x + Random.Range(-frontgroundItems[i].varDistance, frontgroundItems[i].varDistance), -13f, 12f);
                    }
                    else
                    {
                        tempSpawnPoint = new Vector3(frontGroundSpawnPointList[i].transform.position.x + Random.Range(-frontgroundItems[i].varDistance, frontgroundItems[i].varDistance), Random.Range(frontgroundSpawnPoint_Y_MIN, frontgroundSpawnPoint_Y_MAX), Random.Range(-5, 5));
                    }

                    Pooling.Instance.InstantiateFrontground(tempFrontground, tempSpawnPoint);

                    frontGroundSpawnPointListToReach[i].transform.Translate(frontgroundItems[i].spawnDistance, 0, 0);
                }
            }
    }

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawLine(backgroundSpawnPoint.transform.position, backgroundSpawnPointToReach.transform.position);
    //}
}
