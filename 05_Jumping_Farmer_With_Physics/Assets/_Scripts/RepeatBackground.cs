using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPosition;
    private float middlePoint;

    // Start is called before the first frame update
    void Start()
    {
        // Trick: Use a BoxCollider just to measure the background :-)
        middlePoint = this.GetComponent<BoxCollider>().size.x / 2;
        startPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Seamless technique. Reposition backround when it's middle point gets its initial position
        if( this.startPosition.x - this.transform.position.x > middlePoint)
        {
            this.transform.position = startPosition;
        }
    }
}
