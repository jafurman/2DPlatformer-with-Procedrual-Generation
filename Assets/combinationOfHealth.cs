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
        maxHealth = 30f; // Or the total max health of both bosses combined
    }

    void Update()
    {
        if (Boss1 != null)
        {
            healthOne = Boss1.GetComponent<Enemy>().boss1Hp;
        }
        else
        {
            healthOne = 0;
        }

        if (Boss2 != null)
        {
            healthTwo = Boss2.GetComponent<Enemy>().boss2Hp;
        }
        else
        {
            healthTwo = 0;
        }

        float combinedHealth = healthOne + healthTwo;

        if (healthBar != null)
        {
            // Adjust the value to make sure the health bar scales correctly
            float newHealthSize = combinedHealth / maxHealth;
            healthBar.transform.localScale = new Vector3(newHealthSize * 6, .2f, 1);
        }
    }
}
