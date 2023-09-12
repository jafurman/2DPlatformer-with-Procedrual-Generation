using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockFall : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject rock;
    public Vector3 spawnPosition;
    public AudioSource ass;
    public GameObject respawnAnimPrefab; // Prefab for the respawn animation

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeAll; // Lock the rigidbody in place
        spawnPosition = rock.transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(respawn());
        }
    }

    IEnumerator respawn()
    {
        // Wait for 2 seconds, then rock falls
        yield return new WaitForSeconds(1f);

        ass.Play();
        rb.constraints = RigidbodyConstraints2D.None; // Release the rigidbody constraints
        rb.AddForce(Vector2.down * 1.2f, ForceMode2D.Impulse); // Add downward force to simulate dropping

        yield return new WaitForSeconds(1.2f);

        // Instantiate a new respawn animation GameObject
        GameObject respawnAnim = Instantiate(respawnAnimPrefab, spawnPosition, Quaternion.identity);

        yield return new WaitForSeconds(1f);

        // Destroy the respawn animation GameObject
        if (respawnAnim != null)
        {
            Destroy(respawnAnim);
        }

        // Instantiate a new rock GameObject
        Instantiate(rock, spawnPosition, Quaternion.identity);
        Destroy(gameObject);
    }

    void Spawn()
    {
        Instantiate(rock, spawnPosition, Quaternion.identity);
    }
}
