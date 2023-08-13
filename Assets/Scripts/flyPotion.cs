using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyPotion : MonoBehaviour
{

	public PlayerController pc;
	public GameObject yellowPotion;
	public AudioSource playSound;
    public static bool active;

    // Start is called before the first frame update
    private void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        //get the gameobject component
        yellowPotion = gameObject;
    }

    private void OnTriggerEnter2D( Collider2D collision)
     {

     	if ( collision.tag == "Player")
     	{
     		StartCoroutine(fly());
            playSound.Play();
            yellowPotion.GetComponent<Renderer>().enabled = false;
     		
     	}
     }

     IEnumerator fly()
     {
        active = true;

        //get effects sprite
        SpriteRenderer sprite = PlayerController.wings.GetComponent<SpriteRenderer>();

        pc.jumpForce = 4f;
     	pc.theRB2D.gravityScale = 0.5f;


        yield return new WaitForSeconds(18f);

        sprite.enabled = false;
        yield return new WaitForSeconds(.33f);
        sprite.enabled = true;
        yield return new WaitForSeconds(.33f);
        sprite.enabled = false;
        yield return new WaitForSeconds(.33f);
        sprite.enabled = true;
        yield return new WaitForSeconds(.33f);
        sprite.enabled = false;
        yield return new WaitForSeconds(.33f);
        sprite.enabled = true;
        yield return new WaitForSeconds(.33f);
        sprite.enabled = false;

        active = false;



        pc.theRB2D.gravityScale = 1f;
     	pc.jumpForce = 3f;

     	Destroy(gameObject);
     }
}

