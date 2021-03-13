﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float movementForce = 5;
    private Rigidbody _rigidbody;
    public GameObject focalPoint;
    public bool hasPowerUp;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _rigidbody.AddForce(focalPoint.transform.forward * movementForce * Input.GetAxis("Vertical"));

    }

    /// <summary>
    /// Get Power UPs
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            Destroy(other.gameObject);
        }
    }

}
