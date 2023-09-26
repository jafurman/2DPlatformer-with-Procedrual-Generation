using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineAtPlayer : MonoBehaviour
{
    public Rigidbody2D rb;
    private Vector3 playerPos;
    private Vector3 spawnPos;
    private Vector3 direction;

    public float MageShotSpeed = 3.5f;

    public GameObject explodePrefab;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
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
        rb.velocity = direction.normalized * MageShotSpeed;
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
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.TakeDamage(1);
            GameObject explode = explodePrefab;

            Instantiate(explode, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}
