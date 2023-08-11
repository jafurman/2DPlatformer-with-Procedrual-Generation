using UnityEngine;

public class SpinOnZ : MonoBehaviour
{
    public float spinSpeed = 30.0f; // Adjust this value to control the spin speed

    void Update()
    {
        // Rotate the object around the Z-axis
        transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
    }
}
