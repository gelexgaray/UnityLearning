using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targetItems;
    public float spawnRateSeconds = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TargetSpawner());
    }

    /// <summary>
    /// Corrutina que genera objetos cada cierto tiempo
    /// </summary>
    private IEnumerator TargetSpawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRateSeconds);
            var index = Random.Range(0, targetItems.Count);
            Instantiate(targetItems[index]);
        }
    }
}
