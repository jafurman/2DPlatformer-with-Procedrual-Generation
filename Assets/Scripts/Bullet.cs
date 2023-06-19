using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject theBullet;
    public float speed = 1.5f;
    public Rigidbody2D rb;
    public float damage = 3f;
    public GameObject impactEffect;

    public GameObject player; //for the player's info

    public AudioSource playSound;

    private float scaleDecreaseRate = 0.05f; // rate at which to decrease the scale of the game object

    public static float timeDuration = 4;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(timeStop());
    }

    void Update()
    {
        rb.velocity = transform.right * speed;

        if (player != null)
        {
            if (PlayerController.FacingRight == true)
            {
                player.transform.position += new Vector3(1.5f * Time.deltaTime, 0, 0);
            }
            else if (PlayerController.FacingRight == false)
            {
                player.transform.position -= new Vector3(1.5f * Time.deltaTime, 0, 0);
            }
        }

        // decrease the X scale of the game object
        transform.localScale -= new Vector3(scaleDecreaseRate * Time.deltaTime, 0f, 0f);

    }

    public void OnTriggerEnter2D(Collider2D hitInfo)  //when anything happens when bullet hits
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        playSound.Play();
        Instantiate(impactEffect, transform.position, transform.rotation);
    }


    public void OnCollisionEnter2D(Collision2D hitInfo)
    {
        Enemy enemy = hitInfo.collider.GetComponent<Enemy>();

        if (hitInfo.gameObject.tag == "Player")
        {
            PlayerController.canMove = false;
            Debug.Log("Connected Player");
            player = hitInfo.gameObject;
        } else if ( hitInfo.gameObject.tag == "Enemy")
        {
            enemy.TakeDamage(damage);
            //we don't want repetative damage on this
            Destroy(gameObject);
        }
    }

    public void OnCollisionExit2D(Collision2D coal)
    {
        if (coal.gameObject.tag == "Player")
        {
            PlayerController.canMove = true;
            player = null;
        }
    }

    IEnumerator timeStop()
    {
        yield return new WaitForSeconds(timeDuration);

        Destroy(gameObject);
    }
}
