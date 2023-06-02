using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBoss : MonoBehaviour
{
    private Enemy bossInstance;

    public float timeBetweenSpawn = 20f;
    public GameObject eyeballPrefab;
    public Transform spawnPosition1;
    public Transform spawnPosition2;
    public Transform spawnPosition3;

    public bool phase2Enabled;

    private float timer = 0f;
    private bool spawnEyesTriggered = false;

    public Animator bossAnimator;

    public GameObject healthBar;

    // Start is called before the first frame update
    void Start()
    {
        //So I dont have to place the script in the inspector tab, yes I'm that lazy
        bossInstance = GetComponent<Enemy>();

        bossAnimator = GetComponent<Animator>();

        phase2Enabled = false;

        bossAnimator.SetTrigger("Idle");

     
    }

    // Update is called once per frame
    void Update()
    {
        if (bossInstance != null)
        {
            //get the current HP of the boss
            float currentHp = bossInstance.currentHealth;

            //if the boss hp dips below 15 then its time for phase 2
            if ( currentHp <= 15 )
            {
                phase2Enabled = true;
                Debug.Log("It's time for PHASE 2 BITCH");
            }
        }


        if (!phase2Enabled)
        {
            // Logic for every 20-second spawn of eyes
            timer += Time.deltaTime;
            if (timer >= timeBetweenSpawn)
            {
                if (!spawnEyesTriggered)
                {
                    bossAnimator.SetTrigger("SpawnEyes");
                    StartCoroutine(spawnEyes());
                    spawnEyesTriggered = true;
                }
                else
                {
                    bossAnimator.ResetTrigger("SpawnEyes");
                    spawnEyesTriggered = false;
                    timer = 0f;
                }
            }
        }
        }

    public void SpawnEye()
    {
        if((eyeballPrefab != null) && (spawnPosition1 != null) && (spawnPosition2 != null) && (spawnPosition3 != null))
        {
            StartCoroutine(spawnEyes());
        }
       

    }


    public IEnumerator spawnEyes()
    {
        Debug.Log("SPAWN DEM EYES BOI");
        yield return new WaitForSeconds(1.0f);
        Instantiate(eyeballPrefab, spawnPosition1.position, Quaternion.identity);
        Instantiate(eyeballPrefab, spawnPosition2.position, Quaternion.identity);
        yield return new WaitForSeconds(.5f);
        Instantiate(eyeballPrefab, spawnPosition3.position, Quaternion.identity);
    }
}
