using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPotion : MonoBehaviour
{
    public GameObject potion;
    public Transform spawnPoint;

    public bool spawnCondition;

    public void Update()
    {
        //constatntly checking if the potion is active. If it isn't then we cant 
        if (potion.tag == "potionGreen" && greenPotion.active )
        {
            spawnCondition = false;
        }
        else if (potion.tag == "potionBlue" && speedPotion.speedBoostOn)
        {
            spawnCondition = false;
        }
        else if (potion.tag == "potionYellow" && flyPotion.active)
        {
            spawnCondition = false;
        } else
        {
            spawnCondition = true;
        }


    }
    public void OnTriggerEnter2D(Collider2D other)
    {
      
       if ( other.gameObject.tag == "Player" && spawnCondition)
        {
            Instantiate(potion, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
