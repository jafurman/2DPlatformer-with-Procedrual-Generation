using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{


    public static float health = .3f;

    public GameObject deathEffect;

    public AudioSource playSound;

    public GameObject healthBar;

    public AudioSource deathSound;

    public int soulValue;

    private Animator theAnim;

    private bool isDead;

    public Rigidbody2D rb;

    private Vector2 localScale;


public void TakeDamage (float damage)
{
    healthBar.transform.localScale += new Vector3(-.33f, 0f, 0f );
    damage = .3f;
    health -= damage;

    

    if (health <= 0) 
    {
        // isDead = true; 

        //disable movement and collider
        Collider2D hitBox;
         hitBox = GetComponent<Collider2D>();
         hitBox.enabled = false;
         deathSound.enabled = true;
         SinusoidalMove sinMov = GetComponent<SinusoidalMove>();
         if (sinMov != null)
         {
            sinMov.enabled = false;
         }
         Spider spider = GetComponent<Spider>();
         if (spider != null)
         {
            spider.enabled = false; 
         }
         
         StartCoroutine(stopSound());
         
        //theAnim.SetBool("Dead", isDead);
        ScoreManager.instance.ChangeScore(soulValue);
        Die();
        
    } 
}

    IEnumerator stopSound()
    {
        
        yield return new WaitForSeconds(0.45f);
        Debug.Log("Called");
        deathSound.enabled = false;
        Destroy(gameObject);


    }

void Die()
{
   StartCoroutine(stopAnim());
    
}


    IEnumerator stopAnim()
    {
       Instantiate(deathEffect, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(.5f);
        isDead = false; 
        theAnim.SetBool("Dead", isDead);

        
    }



    // Start is called before the first frame update
    void Start()
    {
        
        theAnim = transform.parent.GetComponent<Animator>();
        localScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();

        
    }



}

 