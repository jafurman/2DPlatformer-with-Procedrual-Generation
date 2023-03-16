using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyPotion : MonoBehaviour
{

	public PlayerController pc;
	public GameObject yellowPotion;
	public AudioSource playSound;
    // Start is called before the first frame update
     private void Start(){
          pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
     }

    private void OnTriggerEnter2D( Collider2D collision)
     {

     	if ( collision.tag == "Player")
     	{
     		StartCoroutine(fly());
     		yellowPotion.GetComponent<Renderer>().enabled = false;
     		playSound.Play();

     		

     	}
     }

     IEnumerator fly()
     {
     	Debug.Log("Started Coroutine at timestamp : " + Time.time);

     	pc.jumpForce = 4.5f;
     	pc.theRB2D.gravityScale = 0.3f;
     	Debug.Log(pc.jumpForce);

     	yield return new WaitForSeconds(5);

     	Debug.Log(pc.jumpForce);
     	pc.theRB2D.gravityScale = 1f;
     	pc.jumpForce = 3f;


     	Debug.Log("Finished Coroutine at timestamp : " + Time.time);

     	// Destroy(gameObject);
     }
}

