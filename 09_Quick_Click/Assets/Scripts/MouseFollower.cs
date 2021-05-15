using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    public Camera playerCamera;

    // Update is called once per frame
    void Update()
    {
        var mouseProyectionOnCamera = playerCamera.ScreenToWorldPoint(Input.mousePosition);
        this.transform.position = new Vector3(mouseProyectionOnCamera.x, mouseProyectionOnCamera.y); // playground on z = 0 
    }
}
