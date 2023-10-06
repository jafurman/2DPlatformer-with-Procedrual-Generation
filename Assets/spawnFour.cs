using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnFour : MonoBehaviour
{
    public GameObject spawnPrefab;
    public GameObject respawnPrefab;

    public Transform sp1;
    public Transform sp2;
    public Transform sp3;
    public Transform sp4;

    private int counter;

    private Vector3 spawnPos;


    // Update is called once per frame
    void Update()
    {
        if (counter == 1)
        {
            spawnPos = sp1.position;
        }
        if(counter == 2)
        {
            spawnPos = sp2.position;
        }
        if(counter == 3)
        {
            spawnPos = sp3.position;
        }
        if(counter == 4)
        {
            spawnPos = sp4.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       if (other.CompareTag("Player"))
        {
            StartCoroutine(spawnEm());
        }
    }

    private IEnumerator spawnEm()
    {
        counter = 1;
        int temp = 0;
        while (temp < 4)
        {
            // Instantiate a new respawn animation GameObject
            GameObject respawnAnim = Instantiate(respawnPrefab, spawnPos, Quaternion.identity);

            yield return new WaitForSeconds(1.4f);

            Destroy(respawnAnim);

            GameObject spawnObject = Instantiate(spawnPrefab, spawnPos, Quaternion.identity);
            counter++;
            temp++;
        }

        Destroy(gameObject);
        
    }
}
