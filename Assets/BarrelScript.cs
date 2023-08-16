using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelScript : MonoBehaviour
{ 
    public GameObject brokenBarrel;

    public void spawnBarrel(Vector3 barrelPos)
    {
        Instantiate(brokenBarrel, barrelPos, Quaternion.identity);
        
    }

}

