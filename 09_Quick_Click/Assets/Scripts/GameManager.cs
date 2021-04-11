using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targetItems;
    public float spawnRateSeconds = 1.0f;

    public int Score {
        get { return _score; }
        set {
            _score = Mathf.Clamp(value, 0, 99999);
            _scoreTMP.text = $"Score: {_score}";
        }
    }
    private TextMeshProUGUI _scoreTMP;
    private int _score;

    // Start is called before the first frame update
    void Start()
    {
        _scoreTMP = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        Score = 0;
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
