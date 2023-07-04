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
        //get the players rigidbody
        rb.GetComponent<Rigidbody2D>();
    }


    //while sliding
    void OnTriggerStay2D(Collider2D col)
    {
        Debug.Log("Staying");
        if (col.gameObject.tag == "bullet")
        {
            //do nothing
        }
        else if (col.gameObject.tag == "sliders")
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            slidingAudio.enabled = true;
            isSliding = true;
            animator.SetBool("Sliding", true);
            animator.SetBool("Grounded", false);

            rb.drag = 5f;
            rb.angularDrag = 5f;
            if (Input.GetKey(KeyCode.W))
            {
                PlayerController.canJump = false;
            }

        }

    }
            //while leaving the slide
     void OnTriggerExit2D(Collider2D col)
          {

                Debug.Log("Leave");
                if (col.gameObject.tag == "bullet")
                {
                    //do nothing
                }
                else if (col.gameObject.tag == "sliders")
                {

                    PlayerController.canJump = true;
                    if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
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


  
}

    
