using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeLightsOnEnter : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LightingController.crazyLights();
        }
    }

}
