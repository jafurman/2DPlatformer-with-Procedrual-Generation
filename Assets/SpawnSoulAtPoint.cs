using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSoulAtPoint : MonoBehaviour
{
    public GameObject soul;
    public Transform spawnPoint;

    public void OnTriggerEnter2D(Collider2D other)
    {
       if ( other.gameObject.tag == "Player")
        {
            Instantiate(soul, spawnPoint.position, spawnPoint.rotation);
        }
    }

}
