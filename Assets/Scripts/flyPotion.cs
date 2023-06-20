using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyPotion : MonoBehaviour
{

	public PlayerController pc;
	public GameObject yellowPotion;
	public AudioSource playSound;

    public GameObject playerEffect;

    // Start is called before the first frame update
    private void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        playerEffect = GameObject.FindGameObjectWithTag("yellowPotionEffect");

        if (playerEffect != null)
        {
            //initially set the effect to false
            playerEffect.SetActive(false);
        }

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
     	pc.jumpForce = 5f;
     	pc.theRB2D.gravityScale = 0.4f;

        if (playerEffect != null)
        {
            //initially set the effect to false
            playerEffect.SetActive(true);
        }

        yield return new WaitForSeconds(8f);

        if (playerEffect != null)
        {
            //initially set the effect to false
            playerEffect.SetActive(false);
        }

        pc.theRB2D.gravityScale = 1f;
     	pc.jumpForce = 3f;

     	Destroy(gameObject);
     }
}

