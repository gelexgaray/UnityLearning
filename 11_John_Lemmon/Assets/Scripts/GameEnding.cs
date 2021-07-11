using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1.0f;
    public float displayImageDuration;
    private bool isPlayerAtExit;
    private bool isPlayerCaught;
    public GameObject player;
    public CanvasGroup exitBackgroundImageCanvasGroup;
    public CanvasGroup caughtBackgroundImageCanvasGroup;
    private float timer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            isPlayerAtExit = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerAtExit)
        {
            EndLevel(exitBackgroundImageCanvasGroup, true);
        }
        else if (isPlayerCaught)
        {
            EndLevel(caughtBackgroundImageCanvasGroup, false);
        }
    }

    /// <summary>
    /// Lanza la imagen de fin de partida
    /// </summary>
    /// <param name="imageCanvasGroup">Imagen de fin de partida</param>
    void EndLevel(CanvasGroup imageCanvasGroup, bool exit)
    {
        timer += Time.deltaTime;
        imageCanvasGroup.alpha = Mathf.Clamp(timer / fadeDuration, 0, 1);
        if (timer > fadeDuration + displayImageDuration)
        {
            if (exit) Application.Quit();
            else SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void CatchPlayer() {
        isPlayerCaught = true;
    }
}
