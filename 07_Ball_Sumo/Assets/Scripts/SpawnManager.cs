using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private const float spawnRange = 9f;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 spawnPosition = NewRandomSpawnPosition();
        Instantiate(enemyPrefab, spawnPosition, enemyPrefab.transform.rotation);
    }

    private static Vector3 NewRandomSpawnPosition()
    {
        float posX = Random.Range(-spawnRange, spawnRange);
        float posZ = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnPosition = new Vector3(posX, 0, posZ);
        return spawnPosition;
    }
}
