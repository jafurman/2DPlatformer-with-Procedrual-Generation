using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontSlideCat : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
}
