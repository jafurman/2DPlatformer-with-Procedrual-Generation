using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider : MonoBehaviour
{

	public Rigidbody2D rb;
	public Animator animator;
    public static bool isSliding = false; //can be referenced in other scripts for helpful purposes
    public AudioSource slidingAudio;

    // Start is called before the first frame update
    void Start()
    {
        rb.GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
   void OnCollisionStay2D(Collision2D col)
{
    Debug.Log("Staying");
    if (col.gameObject.tag == "bullet")
    {
        //do nothing
    }
    else if (col.gameObject.tag == "sliders")
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
        StartCoroutine(stopSliding());

        slidingAudio.enabled = true;
        isSliding = true;
        animator.SetBool("Sliding", true);
        animator.SetBool("Grounded", false);

        rb.drag = 5f;
        rb.angularDrag = 5f;

        float t = 0;
        float slideTime = 0.5f; // adjust this value to change the sliding time
        float slideSpeed = 50f; // adjust this value to change the sliding speed

        /*
        while (t < slideTime)
        {
            t += Time.deltaTime;
            float progress = t / slideTime;
            rb.velocity = new Vector2(slideSpeed * progress, rb.velocity.y);
            if (rb.velocity.magnitude < 0.01f)
            {
                slidingAudio.enabled = false;
            }
        }
        */
    }
}

    /*

on collision stay
if tag is wall and input.up
apply velocity up and opposite of the wall

    */
    void OnCollisionExit2D(Collision2D col)
    {
        Debug.Log("Leave");
        if (col.gameObject.tag == "bullet")
        {
            //do nothing
        } else if(col.gameObject.tag == "sliders"){
        //rb.velocity = new Vector2(0.1f, 0.1f);
        if ( Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
          rb.velocity = new Vector3(-5f, 5f, 10f);
        }
            isSliding = false;
            slidingAudio.enabled = false; 
    		animator.SetBool("Sliding", false);
    		rb.drag = 0f;
    		rb.angularDrag = 0f;
        }
    }

    IEnumerator stopSliding()
    {
    	yield return new WaitForSeconds(4f);
    	rb.drag = 0f;
    	rb.angularDrag = 0f;
        isSliding = false; 
    }
}

    
