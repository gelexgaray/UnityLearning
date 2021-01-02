using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float spawnPosZ;
    private float spawnRangeX = 10;
    public GameObject[] objectsToSpawn;
    
    [SerializeField, Range(0f, 10f)] 
    private float warmupTime = 2.0f;

    [SerializeField, Range(0f, 10f)]
    private float spawnTime = 1.0f;

    [SerializeField, Range(0f, 10f)]
    private float spawnTimeDecrement = 0.05f;

    [SerializeField, Range(0f, 10f)]
    private float minSpawnTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        spawnPosZ = this.transform.position.z;
        Invoke("SpawnObject", warmupTime);
    }

    // Spawn a random animal
    void SpawnObject()
    {
        if (objectsToSpawn.Length == 0)
        {
            Debug.LogWarning("SpawnObject called on empty object array");
            return;
        }

        var spawnPosX = Random.Range(-spawnRangeX, spawnRangeX);
        var objectIndex = Random.Range(0, objectsToSpawn.Length);

        Instantiate(
            objectsToSpawn[objectIndex],
            new Vector3(spawnPosX, this.transform.position.y, spawnPosZ),
            this.transform.rotation
            );

        Invoke("SpawnObject", spawnTime);
        spawnTime -= spawnTimeDecrement;
        if (spawnTime <= minSpawnTime) spawnTime = minSpawnTime;
    }
}
