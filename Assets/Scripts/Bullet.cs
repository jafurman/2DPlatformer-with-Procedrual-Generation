using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
public GameObject theBullet;
    public float speed = 20f;
    public Rigidbody2D rb;
    public float damage = .2f; 
    public GameObject impactEffect;

    public GameObject player; //for the player's info

    public AudioSource playSound;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(timeStop());

          //rigidbody, please move right accordinfg to our speed
    }

    void Update()
    {
        rb.velocity = transform.right * speed;

        if(player != null)
            {
                if (PlayerController.FacingRight == true)
                {
                    player.transform.position += new Vector3(1.5f * Time.deltaTime,0,0);
                    } else if (PlayerController.FacingRight == false)
                    {
                    player.transform.position -= new Vector3(1.5f * Time.deltaTime,0,0);
                    }
                
            }
    }

    public void OnTriggerEnter2D(Collider2D hitInfo)  //when anything happens when bullet hits
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        Spider spider = hitInfo.GetComponent<Spider>();
        Bat bat = hitInfo.GetComponent<Bat>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        playSound.Play();
        Instantiate(impactEffect, transform.position, transform.rotation);

        
    }

    public void OnCollisionEnter2D(Collision2D hitInfo)
    {
        if(hitInfo.gameObject.tag == "Player")
        {
            PlayerController.canMove = false;
            Debug.Log("Connected Player");
            player = hitInfo.gameObject;
        }
    }

    public void OnCollisionExit2D(Collision2D coal){
        if(coal.gameObject.tag == "Player")
        {
            PlayerController.canMove = true;
            player = null;
        }
    }

    IEnumerator timeStop()
    {   
        yield return new WaitForSeconds(2.5f);

        Destroy(gameObject);  
    }
}