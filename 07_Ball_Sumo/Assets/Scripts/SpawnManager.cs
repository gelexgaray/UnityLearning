using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerUpPrefab;
    private const float spawnRange = 9f;
    private int waveSize = 1;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Called once per frame
    private void Update()
    {
        int remainingEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (remainingEnemies <= 0) {
            SpawnEnemyWave(waveSize);
            SpawnPowerUps(Random.Range(0, waveSize/2));
            waveSize++;
        }
    }

    private void SpawnPowerUps(int powerUpNumber)
    {
        for( int i = 0; i < powerUpNumber; i++)
        {
            var position = NewRandomSpawnPosition();
            Instantiate(powerUpPrefab, position, powerUpPrefab.transform.rotation);
        }
    }

    private void SpawnEnemyWave(int enemyNumber)
    {
        for (int i = 0; i < enemyNumber; i++)
        {
            Vector3 spawnPosition = NewRandomSpawnPosition();
            Instantiate(enemyPrefab, spawnPosition, enemyPrefab.transform.rotation);
        }
    }

    private static Vector3 NewRandomSpawnPosition()
    {
        float posX = Random.Range(-spawnRange, spawnRange);
        float posZ = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnPosition = new Vector3(posX, 0, posZ);
        return spawnPosition;
    }
}
