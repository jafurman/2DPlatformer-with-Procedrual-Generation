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

    public float yVal, xVal;

    public AudioSource audsrc;

    public AudioClip daClip;
    // Start is called before the first frame update
    void Start()
    {
        
        if (audsrc != null && daClip != null) { audsrc.PlayOneShot(daClip); }

        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        //Vector3 positions of the player and the spawnPoint
        playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        spawnPos = gameObject.transform.position;

        direction = playerPos - spawnPos;

        direction.y += .3f;

        float randomVal = Random.Range(-.4f, .4f);

        direction.y += randomVal;

        StartCoroutine(destroy());

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
            enemy.TakeDamage(3);
            GameObject explode = explodePrefab;

            Instantiate(explode, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }

    private IEnumerator destroy()
    {
        yield return new WaitForSeconds(5f);

        GameObject explode = explodePrefab;

        Instantiate(explode, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
