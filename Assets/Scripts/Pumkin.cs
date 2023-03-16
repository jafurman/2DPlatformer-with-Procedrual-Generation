using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumkin : MonoBehaviour
{
	public GameManager gm;

    private void OnTriggerEnter2D(Collider2D other)
    {
    	if (other.gameObject.tag == "Player")
    	{
    		Debug.Log("EPIC W");
    		gm.Victory();
    	}
    }
}
