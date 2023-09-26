using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cMage : MonoBehaviour
{
    public GameObject shootPrefab;
    public Transform spawnPos;

    private GameObject player;
    private bool hasPlayerPassed = false;

    public BoxCollider2D col;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        col = GetComponent<BoxCollider2D>();

        StartCoroutine(enableCollider());
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            return; // Player not found, do nothing
        }

        // Check if the player's X position is greater than this GameObject's X position
        if (player.transform.position.x > transform.position.x)
        {
            if (!hasPlayerPassed)
            {
                // Flip the GameObject on the X-axis
                FlipObject(true);
                hasPlayerPassed = true;
            }
        }
        else
        {
            if (hasPlayerPassed)
            {
                // Flip the GameObject back on the X-axis
                FlipObject(false);
                hasPlayerPassed = false;
            }
        }
    }

    void FlipObject(bool flip)
    {
        Vector3 newScale = transform.localScale;
        newScale.x = Mathf.Abs(newScale.x) * (flip ? -1 : 1);
        transform.localScale = newScale;
    }

    public void shoot()
    {
        Instantiate(shootPrefab, spawnPos.position, Quaternion.identity);
    }

    public IEnumerator enableCollider()
    {
        col.enabled = false;
        yield return new WaitForSeconds(3.3f);
        col.enabled = true;
    }
}
