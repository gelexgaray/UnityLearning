using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public float speed = 1f;
    public float maxVerticalSpeed = 40f;
    public float maxHorizontalSpeed = 40f;

    void FixedUpdate()
    {
        var verticalSpeed = maxVerticalSpeed * Input.GetAxis("Vertical");
        var horizontalSpeed = maxHorizontalSpeed * Input.GetAxis("Horizontal");

        // Forward at constant speed
        transform.Translate(speed * Time.deltaTime * Vector3.forward );

        // Tilt up/down based on angular speed
        transform.Rotate(verticalSpeed * Time.deltaTime * Vector3.right);

        // Turn left/right with flap animation
        float currentAngle = transform.localEulerAngles.z;
        currentAngle = (currentAngle > 180) ? currentAngle - 360 : currentAngle;
        bool angleOnLimits = System.Math.Abs(currentAngle) < maxHorizontalSpeed;
        bool currentAnglePositive = currentAngle >= 0;
        if ( angleOnLimits || 
             !angleOnLimits && ( 
                  currentAnglePositive && horizontalSpeed > 0 ||
                  !currentAnglePositive && horizontalSpeed < 0))
        {
            transform.Rotate(horizontalSpeed * Time.deltaTime * Vector3.back, Space.Self);
        }

        transform.Translate(horizontalSpeed * Time.deltaTime * Vector3.right, Space.World);


    }
}
