using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineAtPlayer : MonoBehaviour
{
    public Rigidbody2D rb;
    private Vector3 playerPos;
    private Vector3 spawnPos;
    private Vector3 direction;

    public float speed = 5;

    public GameObject explodePrefab;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //Vector3 positions of the player and the spawnPoint
        playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        spawnPos = gameObject.transform.position;

        direction = playerPos - spawnPos;

        direction.y += .3f;

        float randomVal = Random.Range(-.3f, .3f);

        direction.y += randomVal;


    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = direction.normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (explodePrefab != null)
            {
                GameObject explode = explodePrefab;

                Instantiate(explode, transform.position, Quaternion.identity);

                Destroy(gameObject);
            }

        }
    }
}
