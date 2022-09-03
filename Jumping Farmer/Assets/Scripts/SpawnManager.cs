using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject onstaclePrefab;
    public Vector3 spwanLoaction = new Vector3(25,1.3f,0);
    public float spawnDelay = 2;
    public float spawnIntetval = 2;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacle", spawnDelay, spawnIntetval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnObstacle()
    {
        Instantiate(onstaclePrefab, spwanLoaction, onstaclePrefab.transform.rotation);
    }
}
