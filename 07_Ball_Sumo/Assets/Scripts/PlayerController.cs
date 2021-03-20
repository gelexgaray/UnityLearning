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

    private GameObject _powerUpRoot;
    public GameObject[] _powerUpRings;


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        if (_powerUpRings.Length > 0) _powerUpRoot = _powerUpRings[0].transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        _rigidbody.AddForce(focalPoint.transform.forward * movementForce * Input.GetAxis("Vertical"));
        _powerUpRoot.transform.position = this.transform.position;
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
        if (_powerUpRings.Length <= 0)
        {
            // Timed powerup, without feedback
            yield return new WaitForSeconds(powerUpSeconds);
            hasPowerUp = false;
        }
        else
        {
            // Enable all power up rings, and then disable one by one
            float timePerRing = powerUpSeconds / _powerUpRings.Length;
            foreach (GameObject ring in _powerUpRings) ring.SetActive(true);
            foreach (GameObject ring in _powerUpRings)
            {
                yield return new WaitForSeconds(timePerRing);
                ring.SetActive(false);
            }
            hasPowerUp = false;
        }
    }
}
