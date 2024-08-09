using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combinationOfHealth : MonoBehaviour
{
    public GameObject Boss1, Boss2;
    public GameObject healthBar; 
    public float healthOne, healthTwo, maxHealth;

    void Start()
    {
        
        maxHealth = Boss1.GetComponent<Enemy>().maxHealth + Boss2.GetComponent<Enemy>().maxHealth;

    }

    void Update()
    {

        healthOne = Boss1.GetComponent<Enemy>().boss1Hp;
        healthTwo = Boss2.GetComponent<Enemy>().boss2Hp;


        float combinedHealth = healthOne + healthTwo;

        if (healthBar != null)
        {
            float newHealthSize = combinedHealth / maxHealth * 10; 
            healthBar.transform.localScale = new Vector3(newHealthSize, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
        }
    }
}
