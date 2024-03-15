using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelScript : MonoBehaviour
{ 
    public GameObject brokenBarrel;
    public GameObject soul, lifePotion;

    public void spawnBarrel(Vector3 barrelPos)
    {
        int randomNumber = Random.Range(0, 6);
        if (randomNumber == 2 || randomNumber == 5)
        {
            Instantiate(soul, barrelPos, Quaternion.identity);
        }
        if (randomNumber == 4)
        {
            Instantiate(lifePotion, barrelPos, Quaternion.identity);
        }

        Instantiate(brokenBarrel, barrelPos, Quaternion.identity);
        
    }

}

