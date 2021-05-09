using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targetItems;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public GameObject gameSelectionPanel;

    public static GameManager Instance {
        get {
            if( null == _instance) _instance = FindObjectOfType<GameManager>();
            return _instance;
        }
    }
    private static GameManager _instance;

    public int Score {
        get { return _score; }
        private set 
        {
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
            restartButton.gameObject.SetActive(_gameOver);
        }
    }
    private bool _gameOver;

    public void StartGame(float spawnRateSeconds, float gravityFactor = 1.0f)
    {
        Physics.gravity = new Vector3(
            0, 
            Physics.gravity.y * gravityFactor, 
            0);
        gameSelectionPanel.gameObject.SetActive(false);
        Score = 0;
        StartCoroutine(TargetSpawner(spawnRateSeconds));
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Corrutina que genera objetos cada cierto tiempo
    /// </summary>
    private IEnumerator TargetSpawner(float spawnRateSeconds)
    {
        while (!GameOver)
        {
            yield return new WaitForSeconds(spawnRateSeconds);
            var index = Random.Range(0, targetItems.Count);
            Instantiate(targetItems[index]);
        }
    }

    public void TargetPicked(Target target)
    {
        if (GameOver) return;

        if (target.gameObject.CompareTag("Good"))
        {
            Score += target.points;
        }
        else
        {
            Score -= target.points;
        }
    }

    public void TargetDropped(Target target)
    {
        if (GameOver) return;

        if (target.gameObject.CompareTag("Good"))
        {
            GameOver = true;
        }


    }
}
