using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class StopSoundOnGameOver : MonoBehaviour
{
    private AudioSource _audioSource;
    private PlayerController _playerController;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = this.GetComponent<AudioSource>();
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (_playerController.GameOver) _audioSource.Stop();
    }
}
