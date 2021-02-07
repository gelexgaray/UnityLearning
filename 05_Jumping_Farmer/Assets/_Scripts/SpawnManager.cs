using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject[] objectsToSpawn;
    
    [SerializeField, Range(0, 10)] 
    private float secondsForFirstObject = 2;
    
    [SerializeField, Range(0, 10)] 
    private float avgSecondsForNextObject = 2;
    
    [SerializeField, Range( 0, 10)] 
    private float randomSecondsDeltaForNextObject = 1.5f;

    private Vector3 spawnPosition;

    private PlayerController _playerController;

    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = this.transform.position;
        Invoke("SpawnObject", secondsForFirstObject);
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        
    }

    // Update is called once per frame
    void SpawnObject()
    {
        // Stop condtions
        if (objectsToSpawn.Length <= 0)
        {
            Debug.LogWarning("SpawnObject invoked, but array of objects to spawn is empty");
            return;
        }
        else if (_playerController.GameOver) 
        {
            Debug.Log("Game Over: Stop spawning objects");
            return;
        }

        // Spawn object
        GameObject objectToSpawn = objectsToSpawn[ Random.Range(0, objectsToSpawn.Length)];
        Instantiate(objectToSpawn, this.transform);

        Invoke("SpawnObject", 
            avgSecondsForNextObject 
            + Random.Range( -randomSecondsDeltaForNextObject, randomSecondsDeltaForNextObject));

    }
}
