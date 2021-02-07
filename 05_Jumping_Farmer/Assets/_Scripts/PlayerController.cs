using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    #region Physics
    private Rigidbody _rigidBody;
    public float jumpImpulse = 8;
    private bool isOnGround = true;
    #endregion

    #region Animation
    private const string ANIMATION_SPEED = "Speed_f";
    private const string JUMP_TRIGGER = "Jump_trig";
    private const string DEATH_ANIMATION = "Death_b";
    private Animator _animator;
    #endregion

    #region Particles
    [SerializeField, Range(0, 2)]
    private float trailThreshold = 1.5f;
    public ParticleSystem explossion;
    public ParticleSystem trail;
    #endregion

    #region Audio
    private AudioSource _audioSource;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    [SerializeField, Range(0, 1)]
    private float soundVolume = 1;
    #endregion

    #region UI
    public TextMeshProUGUI scoreDisplay;
    #endregion

    public bool GameOver { get; private set; }

    private float GameSpeed {
        get => _gameSpeed;
        set { 
            _gameSpeed = value;
            Time.timeScale =_gameSpeed;

            if (!GameOver)
            {
                float score = (value - 1) * 100;
                scoreDisplay.text = string.Format("{0:N0} $", score);
            }
        } 
    }
    private float _gameSpeed;


    // Start is called before the first frame update
    void Start()
    {
        GameSpeed = 1;
        _rigidBody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ShowTrailIfRunningHighSpeed();
        if (GameOver) GameOverController();
        else GameController();
    }

    private void GameController()
    {
        if (isOnGround && Tap())
        {
            _rigidBody.AddForce(Vector3.up * jumpImpulse, ForceMode.Impulse);
            _audioSource.PlayOneShot(jumpSound, soundVolume);
            isOnGround = false;
            _animator.SetTrigger(JUMP_TRIGGER);
        }

        // Increase game speed over time
        GameSpeed = 1 + Time.timeSinceLevelLoad / 50;
    }

    private void GameOverController()
    {
        if (Tap())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private static bool Tap()
    {
        return (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0));
    }

    private void ShowTrailIfRunningHighSpeed() 
    {
        if (null == trail) return;
        bool enableTrail = (GameSpeed >= trailThreshold && isOnGround && !GameOver);

        if (!trail.isPlaying && enableTrail ) trail.Play();
        else if (trail.isPlaying && !enableTrail) trail.Stop();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (GameOver) return;

        // use CompareTag instead of string comparison to avoid heap instances duplication
        // https://answers.unity.com/questions/200820/is-comparetag-better-than-gameobjecttag-performanc.html
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameOver = true;
            GameSpeed = 1;
            explossion.Play();
            _audioSource.PlayOneShot(crashSound);
            _animator.SetBool(DEATH_ANIMATION, true);
        }
        
    }
}
