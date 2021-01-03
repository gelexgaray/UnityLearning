using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    [SerializeField] 
    private  GameObject dogPrefab;

    [SerializeField, Range(0f, 2f)]
    private float allowedSecondsBetweenShots = 1.0f;
    private float lastDogReleaseTime;


    // Update is called once per frame
    void Update()
    {
        float currentTime = Time.realtimeSinceStartup;

        // On spacebar press, send dog
        // Only send a new dog when the time delta from previous one is < N seconds
        if (Input.GetKeyDown(KeyCode.Space) && (currentTime - lastDogReleaseTime) > allowedSecondsBetweenShots)
        {
            lastDogReleaseTime = Time.realtimeSinceStartup;
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
        }
    }
}
