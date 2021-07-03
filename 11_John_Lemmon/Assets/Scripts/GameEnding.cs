using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1.0f;
    public float displayImageDuration;
    private bool isPlayerAtExit;
    public GameObject player;
    public CanvasGroup exitBackgroundImageCanvasGroup;
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
            timer += Time.deltaTime;
            exitBackgroundImageCanvasGroup.alpha = Mathf.Clamp( timer / fadeDuration, 0, 1);
            if (timer > fadeDuration + displayImageDuration)
            {
                EndLevel();
            }
        }
    }

    void EndLevel()
    {
        Application.Quit();
    }
}
