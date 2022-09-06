using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] onstaclePrefab;
    public float spawnDelay = 5;
    public float spawnIntetval = 2;
    private PlayerControl playerControlScript;
    // Start is called before the first frame update
    void Start()
    {
        playerControlScript = GameObject.Find("Player").GetComponent<PlayerControl>();
        InvokeRepeating("SpawnObstacle", spawnDelay, spawnIntetval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnObstacle()
    {
        if (!playerControlScript.gameOver && playerControlScript.reaStratPos)
        {
            int obstacleIndex = Random.Range(0, onstaclePrefab.Length);
            Vector3 spwanLoaction = new Vector3(34, onstaclePrefab[obstacleIndex].transform.position.y, 0);
            Instantiate(onstaclePrefab[obstacleIndex], spwanLoaction, onstaclePrefab[obstacleIndex].transform.rotation);
        }
    }
}
