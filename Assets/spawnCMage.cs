using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnCMage : MonoBehaviour
{

    public GameObject cMage;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            cMage.SetActive(true);
        }
    }
}
