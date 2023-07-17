using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soulCatcher : MonoBehaviour
{

    public GameObject player; 
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
       if (col.gameObject.CompareTag("bullet"))
        {
            //set the new current position of the player to be the "start" which would resemble a checkpoint
            GameManager.playerStart = player.transform.position;


        }

    }
}
