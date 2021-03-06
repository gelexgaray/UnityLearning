using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float movementForce = 5;
    private Rigidbody _rigidbody;
    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        var targetDirection = (target.transform.position - this.transform.position).normalized;
        _rigidbody.AddForce(targetDirection * movementForce);

    }

}
