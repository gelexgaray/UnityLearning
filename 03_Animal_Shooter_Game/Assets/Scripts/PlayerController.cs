using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontalInput;
    public float speed = 10f;
    public float xRange = 15f;
    public GameObject projectile;
    private GameObject lastProjectile;

    void Update()
    {
        // Movement
        horizontalInput = Input.GetAxis("Horizontal");
        this.transform.Translate(speed * horizontalInput * Time.deltaTime * Vector3.right);

        if (this.transform.position.x < -xRange)
        {
            this.transform.position = new Vector3(
                -xRange,
                this.transform.position.y,
                this.transform.position.z);
        }
        if (this.transform.position.x > xRange)
        {
            this.transform.position = new Vector3(
                xRange,
                this.transform.position.y,
                this.transform.position.z);
        }

        // Fire projectiles 
        if (Input.GetKeyDown(KeyCode.Space) && CanLaunchMoreProjectiles())
        {
            lastProjectile = Instantiate(projectile, this.transform.position, this.transform.rotation);
        }
    }

    private bool CanLaunchMoreProjectiles()
    {
        // Can only launch projectile when the previous one was destroyed
        return (lastProjectile == null || !lastProjectile.activeInHierarchy);
    }
}
