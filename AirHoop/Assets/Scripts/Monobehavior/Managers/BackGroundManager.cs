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
    [SerializeField] private List<GameObject> frontMountainPrefab;
    private float backgroundPrefabSizeX;

    [SerializeField] private float firstSpawnPointX = 40f;
    private float nextSpawnPointX;

    public delegate void OnLevelCreation(List<GameObject> newBackgroundList);
    public static event OnLevelCreation onLevelCreation;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        onLevelCreation(backgroundPrefab);

        backgroundSpawnPoint = new GameObject("Background Spawn Point");
        backgroundSpawnPoint.transform.SetParent(player.transform);
        backgroundSpawnPoint.transform.Translate(offSetSpawn, 0, 0);

        backgroundSpawnPointToReach = new GameObject("Background Spawn Point To Reach");
        backgroundSpawnPointToReach.transform.Translate(backgroundSpawnPoint.transform.position.x, 0, 0);

        nextSpawnPointX = firstSpawnPointX;
    }

    void Update()
    {
        SpawnBackground();
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

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawLine(backgroundSpawnPoint.transform.position, backgroundSpawnPointToReach.transform.position);
    //}
}
