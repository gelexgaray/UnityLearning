using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targetItems;
    public float spawnRateSeconds = 1.0f;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;

    public int Score {
        get { return _score; }
        set {
            _score = Mathf.Clamp(value, 0, 99999);
            scoreText.text = $"Score: {_score}";
        }
    }
    private int _score;

    public bool GameOver {
        get { return _gameOver; }
        private set
        {
            _gameOver = value;
            gameOverText.gameObject.SetActive(_gameOver);
        }
    }
    private bool _gameOver;

    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        StartCoroutine(TargetSpawner());
        // TODO: End game with GameOver = true;
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
