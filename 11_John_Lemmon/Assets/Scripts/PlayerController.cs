using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 movement;
    private Animator _animator;
    private Rigidbody _rigidbody;
    private Quaternion rotation = Quaternion.identity;

    [SerializeField, Range(1, 100)]
    private float turnSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Called at 50fps fixedrate, before OnAnimatorMove
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // We sum two movements of module 1, so the result can be > 1. Normalization is required
        this.movement.Set(horizontal, 0, vertical);
        this.movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0);
        bool isWalking = hasHorizontalInput || hasVerticalInput;

        _animator.SetBool("IsWalking", isWalking);
        Vector3 desiredForward = Vector3.RotateTowards(
            transform.forward,
            movement,
            turnSpeed * Time.fixedDeltaTime,
            0f);
        this.rotation = Quaternion.LookRotation(desiredForward);
    }

    private void OnAnimatorMove()
    {
        // S = S0 * delta S
        // Animation is setup to give the movement quantity. We add the movement direction
        _rigidbody.MovePosition(_rigidbody.position + this.movement * _animator.deltaPosition.magnitude);
        _rigidbody.MoveRotation(this.rotation);
    }
}
