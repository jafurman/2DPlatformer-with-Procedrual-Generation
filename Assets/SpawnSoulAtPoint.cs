using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSoulAtPoint : MonoBehaviour
{
    public GameObject soul;
    public Transform spawnPoint;
    public Transform spawnPoint2;

    public Animator anim;

    public void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            anim.SetTrigger("copy");
        }
    }

    public void SpawnSouls()
    {
        Instantiate(soul, spawnPoint.position, spawnPoint.rotation);
        Instantiate(soul, spawnPoint2.position, spawnPoint.rotation);
    }

}
