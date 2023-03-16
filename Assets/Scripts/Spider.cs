using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    public GameManager gm;
    public LivesManager lm;

    private float dirY;
    private float moveSpeed;
    private Rigidbody2D rb;
    private Vector2 localScale;

    private bool FacingDown = true;

    private Animator theAnim;
    private bool isDead;

    public int soulValue = 1; 

    public static float health = .5f;

    public GameObject deathEffect;

    public AudioSource playSound;

    public GameObject healthBar;

        void Start()
    {

        theAnim = transform.parent.GetComponent<Animator>();
        localScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        dirY = 1f;
        moveSpeed = 1f;

        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
    	if (collision.gameObject.tag == "Player")
    	{
            playSound.Play();
            gm.Reset();
    		lm.TakeLife();
        
        }

        if (collision.GetComponent<Wall>())
        {

            dirY *= -1f;
            Flip();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2( rb.velocity.x, dirY * moveSpeed);
    }
    private void Flip()
    {

        FacingDown = !FacingDown;

        transform.Rotate(180f, 0f, 0f);
    }

    public void fakeDamageForHealtBar( float damage2 )
     {

        Debug.Log(health);
        healthBar.transform.localScale += new Vector3(-.33f, 0f, 0f );
     	damage2 = .2f;
    health -= damage2;

     }

    

    
}
