using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Target : MonoBehaviour
{
    public int points = 1;
    public ParticleSystem explossionParticleSystem;

    private Rigidbody rigidBody;


    // Start is called before the first frame update
    void Start()
    {

        this.transform.position = NewRandomStartPosition();
        rigidBody = this.GetComponent<Rigidbody>();
        rigidBody.AddForce(NewRandomImpulse(), ForceMode.Impulse);
        rigidBody.AddTorque(NewRandomTorque(), ForceMode.Impulse);
    }

    private Vector3 NewRandomStartPosition()
    {
        const float maxSpawnPosX = 4;
        const float spawnPosY = -2;
        var vector3 = new Vector3(Random.Range(-maxSpawnPosX, maxSpawnPosX), spawnPosY);
        return vector3;
    }

    private Vector3 NewRandomImpulse()
    {
        const float minImpulse = 10;
        const float maxImpulse = 17;
        var vector3 = Vector3.up * Random.Range(minImpulse, maxImpulse);
        return vector3;
    }

    private Vector3 NewRandomTorque()
    {
        const float maxImpulse = 10;
        var vector3 = new Vector3(
            Random.Range(0, maxImpulse), 
            Random.Range(0, maxImpulse), 
            Random.Range(0, maxImpulse));
        return vector3;
    }

    private void OnMouseOver()
    {

        GameManager.Instance.TargetPicked(this);
        if (null != explossionParticleSystem) {
            Instantiate(explossionParticleSystem, this.transform.position, this.transform.rotation)
                .Play();
        };
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Destroy when entering sensor at bottom
        if(other.gameObject.CompareTag("KillZone"))
        {
            GameManager.Instance.TargetDropped(this);
            Destroy(this.gameObject);
        }
    }
}
