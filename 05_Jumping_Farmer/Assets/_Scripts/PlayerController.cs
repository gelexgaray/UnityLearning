using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private const string ANIMATION_SPEED = "Speed_f";
    private const string JUMP_TRIGGER = "Jump_trig";
    private const string DEATH_ANIMATION = "Death_b";
    private Animator _animator;
    private Rigidbody _rigidBody;

    [SerializeField, Range(0, 2)]
    private float trailThreshold = 1.5f;

    public float jumpImpulse = 8;
    private bool isOnGround = true;

    public ParticleSystem explossion;
    public ParticleSystem trail;

    public bool GameOver { get; private set; }

    private float GameSpeed {
        get => _gameSpeed;
        set { 
            _gameSpeed = value;
            Time.timeScale =_gameSpeed;
        } 
    }
    [SerializeField]
    private float _gameSpeed;


    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ShowTrailIfRunningHighSpeed();
        if (GameOver) return; 

        // The following code runs only on active game play
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            _rigidBody.AddForce(Vector3.up * jumpImpulse, ForceMode.Impulse);
            isOnGround = false;
            _animator.SetTrigger(JUMP_TRIGGER);
        }

        // Increase game speed over time
        GameSpeed = 1 + Time.time / 50;
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
        // use CompareTag instead of string comparison to avoid heap instances duplication
        // https://answers.unity.com/questions/200820/is-comparetag-better-than-gameobjecttag-performanc.html
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Time.timeScale = 1;
            if (null != explossion) explossion.Play();
            _animator.SetBool(DEATH_ANIMATION, true);
            GameOver = true;
        }
        
    }
}
