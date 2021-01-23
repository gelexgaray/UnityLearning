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

    public float jumpImpulse = 10;
    private bool isOnGround = true;

    public bool GameOver { get; private set; }


    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

        _animator.SetFloat(ANIMATION_SPEED, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            _rigidBody.AddForce(Vector3.up * jumpImpulse, ForceMode.Impulse);
            isOnGround = false;
            _animator.SetTrigger(JUMP_TRIGGER);
        }
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
            _animator.SetBool(DEATH_ANIMATION, true);
            GameOver = true;
        }
    }
}
