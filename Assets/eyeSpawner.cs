using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eyeSpawner : MonoBehaviour
{

    public GameObject e1,e2,e3,e4;
    // Start is called before the first frame update
    void Start()
    {
        e1.SetActive(false);
        e2.SetActive(false);
        e3.SetActive(false);
        e4.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            e1.SetActive(true);
            e2.SetActive(true);
            e3.SetActive(true);
            e4.SetActive(true);
        }
    }

}
