using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    public float topBound = 30f;
    public float bottomBound = -1f;

    void Update()
    {
        // Destroy projectiles arriving top bound
        if (this.transform.position.z > topBound)
        {
            Destroy(this.gameObject);
        }

        // Some enemy arrived bottom bound: Game Over!
        if (this.transform.position.z < bottomBound)
        {
            Debug.Log("Game Over!");
            Time.timeScale = 0;
            Destroy(this.gameObject);
        }
    }
}
