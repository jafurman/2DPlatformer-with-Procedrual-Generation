using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnScreenPointsBehavior : MonoBehaviour
{
    public float floatingSpeed = 1.0f;
    public float angleVariation = 20.0f;

    private void Start()
    {
        StartCoroutine(FloatingRoutine());
    }

    IEnumerator FloatingRoutine()
    {
        while (true)
        {
            // Calculate a random launch angle within the specified variation
            float launchAngle = Random.Range(-angleVariation / 2, angleVariation / 2);

            // Calculate the direction based on the launch angle
            Vector2 direction = Quaternion.Euler(0, 0, launchAngle) * Vector2.up;

            // Move the object upwards
            transform.Translate(direction * floatingSpeed * Time.deltaTime);

            yield return null;
        }
    }
}