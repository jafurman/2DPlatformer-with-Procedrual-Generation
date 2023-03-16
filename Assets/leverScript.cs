using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leverScript : MonoBehaviour
{

	public GameObject gate;
	public GameObject c;
	public bool hasActivated = false;
	public Collider2D gateCollider;

	public float whereToStopGateOnY;

	public bool moving = false;
	public bool stoppedMoving = false;

	//Audio
	public AudioSource gateOpen;
	public AudioSource gateConstant;
	public AudioSource gateStop;

    // Start is called before the first frame update
    void Start()
    {
        Collider2D gateCollider = GetComponent<Collider2D>();

        //To flip the switch were just going to flip the sprite 180 degrees
        Transform transform = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float currentY = gate.transform.position.y;
        if (moving)
        {
        	gate.transform.position = new Vector2(gate.transform.position.x, currentY + .5f * Time.deltaTime);
        	gateConstant.enabled = true;
        }
    		//only up 2 spaces so it stops after that
    		if (gate.transform.position.y >= whereToStopGateOnY)
    			{
    				gateConstant.enabled = false;
    				gateStop.enabled = true;
    				moving = false;
    			}
    }

    void OnTriggerStay2D(Collider2D col)
    {
    	if ( col.gameObject.tag == "Player" && !hasActivated)
    	{
    		//set message active to ask player if they want to click
    		c.SetActive(true);
    		
    		if (Input.GetKey(KeyCode.P))
    		{
    			//flip switch
    			transform.localScale = new Vector2(transform.localScale.x, -transform.localScale.y);
    			gateOpen.Play();
    			//start moving the platform
    			moving = true;
    			//only one activation allowed
    			hasActivated = true; 
    			//set canvas false
    			c.SetActive(false);
    			//turn off the gate's collider
    			gateCollider.enabled = false;
    			//Transform the gates position up 

    		}
    	}
    }

    void OnTriggerExit2D(Collider2D col)
    {
    	c.SetActive(false);
    }

}
