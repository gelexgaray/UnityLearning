using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField(), Range(0f, 30f)]
    [Tooltip("Max speed in m/s")]
    private float maxSpeed = 20f;

    [SerializeField(), Range(0f, 90f)]
    [Tooltip("Max turn angle")]
    private float maxTurnSpeed = 45f;

    [SerializeField(), Range(0f, 1f)]
    [Tooltip("Min linear speed to enable turning")]
    private float minSpeedToTurn = 1f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float linearSpeed = maxSpeed * Input.GetAxis("Vertical");
        float angularSpeed  = maxTurnSpeed * Input.GetAxis("Horizontal");
        this.transform.Translate(linearSpeed * Time.deltaTime * Vector3.forward);
        if (System.Math.Abs(linearSpeed) > minSpeedToTurn) 
            this.transform.Rotate(angularSpeed * Time.deltaTime * Vector3.up);
    }
}
