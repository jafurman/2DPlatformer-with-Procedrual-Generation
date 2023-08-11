using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpinScript : MonoBehaviour
{
    public float spinSpeed = 30.0f; // Adjust this value to control the spin speed

    void Update()
    {
        // Rotate the object around the X-axis
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
    }
}

