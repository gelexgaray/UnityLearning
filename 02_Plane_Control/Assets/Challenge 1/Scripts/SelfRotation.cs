using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfRotation : MonoBehaviour
{
    public Vector3 rotationVector = Vector3.forward;
    public float rotationSpeed = 100000f;

    void Update()
    {
        this.transform.Rotate(rotationSpeed * Time.deltaTime * rotationVector);
    }
}
