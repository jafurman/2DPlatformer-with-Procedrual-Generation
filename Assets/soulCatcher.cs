using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soulCatcher : MonoBehaviour
{
    public Animator anim;
    public bool active;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        anim = GetComponent<Animator>();

        //initialize active to false
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ( anim != null)
        {
            //if the player hits the thing, we then activate the visual indication of if the flag is set
            if (active)
            {
                anim.SetBool("activated", true);
            }
            else
            {
                anim.SetBool("activated", false);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
       if (col.gameObject.CompareTag("bullet"))
        {
            //set the new current position of the player to be the "start" which would resemble a checkpoint
            GameManager.playerStart = player.transform.position;

            //set the active value true
            active = true;

        }

    }
}
