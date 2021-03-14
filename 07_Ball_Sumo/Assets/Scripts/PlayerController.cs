using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float movementForce = 5;
    private Rigidbody _rigidbody;
    public GameObject focalPoint;
    public bool hasPowerUp;
    public float powerUpRepulseForce = 100;
    public float powerUpSeconds = 7;

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
            StartCoroutine(PowerUpExpirer());
        }
    }

    /// <summary>
    /// Apply PowerUps
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy") && hasPowerUp)
        {
            var enemyRigidBody = collision.collider.gameObject.GetComponent<Rigidbody>();
            Vector3 repulseDirection = collision.collider.transform.position - this.transform.position;
            enemyRigidBody.AddForce(repulseDirection * powerUpRepulseForce, ForceMode.Impulse);
        }
    }

    IEnumerator PowerUpExpirer() 
    {
        yield return new WaitForSeconds(powerUpSeconds);
        hasPowerUp = false;
    }
}
