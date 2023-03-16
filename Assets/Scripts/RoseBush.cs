using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoseBush : MonoBehaviour
{

	    public GameManager gm;
    	public LivesManager lm;

    	
       public void OnTriggerEnter2D(Collider2D collision)
    {
    	if (collision.gameObject.tag == "Player")
    	{
           
            gm.Reset();
    		lm.TakeLife();
        
        }
    }
}
