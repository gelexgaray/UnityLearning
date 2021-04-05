using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Target : MonoBehaviour
{
    private Rigidbody _rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = NewRandomStartPosition();
        _rigidBody = this.GetComponent<Rigidbody>();
        _rigidBody.AddForce(NewRandomImpulse(), ForceMode.Impulse);
        _rigidBody.AddTorque(NewRandomTorque(), ForceMode.Impulse);
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
        const float maxImpulse = 18;
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

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        // Destroy when entering sensor at bottom
        if(other.gameObject.name.Equals("Sensor"))
        {
            Destroy(this.gameObject);
        }
    }
}
