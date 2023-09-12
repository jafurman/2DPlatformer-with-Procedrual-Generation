using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider : MonoBehaviour
{

    public Rigidbody2D rbi;
    public Animator animator;
    public static bool isSliding = false; //can be referenced in other scripts for helpful purposes
    public AudioSource slidingAudio;

    public static bool CanJump;

    public Collider2D col;

    public static bool HaveNotJumped;

    // Start is called before the first frame update
    void Start()
    {
        //get the players rigidbody
        rbi.GetComponent<Rigidbody2D>();
        col.GetComponent<Collider2D>();
    }


    //while sliding
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "bullet")
        {
            //do nothing
        }
        else if (col.gameObject.tag == "sliders")
        {
            rbi.velocity = new Vector2(0, rbi.velocity.y);
            slidingAudio.enabled = true;
            isSliding = true;
            animator.SetBool("Sliding", true);
            animator.SetBool("Grounded", false);

            rbi.drag = 5f;
            rbi.angularDrag = 5f;
            if (Input.GetKey(KeyCode.W))
            {
                PlayerController.canJump = false;
            }

        }


    }

    private void Update()
    {
        if (col.enabled == false && HaveNotJumped)
        {
            col.enabled = true;
        }
    }

    //while leaving the slide
    void OnTriggerExit2D(Collider2D col)
    {
            if (col.gameObject.tag == "bullet")
            {
                //do nothing
            }
            else if (col.gameObject.tag == "sliders")
            {
                PlayerController.canJump = true;
                if ((Input.GetKey(KeyCode.A) && !PlayerController.FacingRight) || (Input.GetKey(KeyCode.D) && PlayerController.FacingRight))
                {
                    StartCoroutine(setActiveCollider());
                    rbi.velocity = new Vector3(-5f, 5f, 10f);
                }

                isSliding = false;
                slidingAudio.enabled = false;
                animator.SetBool("Sliding", false);
                rbi.drag = 0f;
                rbi.angularDrag = 0f;
            }

        
        
    }


    private IEnumerator setActiveCollider()
    {
        col.enabled = false;
        HaveNotJumped = false;
        yield return new WaitForSeconds(.6f);
        HaveNotJumped = true;
        col.enabled = true;
    }




    }


 

    
