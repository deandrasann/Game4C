using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn;
    public float minSpawnInterval = 2f; 
    public float maxSpawnInterval = 5f; 
    public Transform spawnPoint; 
    public float spawnRadius = 5f; 
    public float firstSpawnTime = 2f;

    private float nextSpawnTime; 

    private bool hasSpawnedFirstObject = false;
    

    void Start()
    {
        nextSpawnTime = Time.time + firstSpawnTime;
    }

    void Update()
    {
        if (!hasSpawnedFirstObject && Time.time >= nextSpawnTime)
        {
            SpawnObject();
            hasSpawnedFirstObject = true;
            //nextSpawnTime += spawnInterval; 
            nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
        }
        else if (hasSpawnedFirstObject && Time.time >= nextSpawnTime)
        {
            SpawnObject();
            nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
        }
    }

    void SpawnObject()
    {
        GameObject objectToSpawn = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];

        Vector3 spawnPosition = spawnPoint.position + Random.insideUnitSphere * spawnRadius;

        spawnPosition.y = spawnPoint.position.y;

        Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
    }
}
