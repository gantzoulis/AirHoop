using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    private GameObject groundSpawnPoint;
    private GameObject groundSpawnPointToReach;
    [SerializeField] private float offSetSpawn = 60f;

    private GameObject player;

    [SerializeField] private List<GameObject> groundPrefab;
    private float groundPrefabSizeX;

    [SerializeField] private float firstSpawnPointX = 40f;
    private float nextSpawnPointX;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        groundSpawnPoint = new GameObject("Ground Spawn Point");
        groundSpawnPoint.transform.SetParent(player.transform);
        groundSpawnPoint.transform.Translate(offSetSpawn, 0, 0);

        groundSpawnPointToReach = new GameObject("Ground Spawn Point To Reach");
        groundSpawnPointToReach.transform.Translate(groundSpawnPoint.transform.position.x, 0, 0);

        nextSpawnPointX = firstSpawnPointX;
    }

    void Update()
    {
        SpawnGround();
    }

    private void SpawnGround()
    {
        GameObject tempGround = groundPrefab[Random.Range(0, groundPrefab.Count)];

        groundPrefabSizeX = tempGround.GetComponent<BoxCollider>().size.x;

        if (groundSpawnPoint.transform.position.x >= groundSpawnPointToReach.transform.position.x)
        {
            Vector3 tempSpawnPoint = new Vector3(nextSpawnPointX, -12f , 0f);

            Pooling.Instance.InstantiateGround(tempGround, tempSpawnPoint);

            groundSpawnPointToReach.transform.Translate(groundPrefabSizeX, 0, 0);

            nextSpawnPointX += groundPrefabSizeX;
        }
    }
}
