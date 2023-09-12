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

    public GameObject itemsToDestroy;

    public LayerMask attackMask;
    public float attackRange = .8f;

    public PlayerController pc;

    public GameObject leverForDoor;

    public GameObject deathAnimPrefab;

    public Transform spawnDeathPos;

    private bool bossDead = false;

    // Start is called before the first frame update
    void Start()
    {
        pc = GameObject.FindObjectOfType<PlayerController>();

        //So I dont have to place the script in the inspector tab, yes I'm that lazy
        bossInstance = GetComponent<Enemy>();

        bossAnimator = GetComponent<Animator>();

        phase2Enabled = false;

        bossAnimator.SetTrigger("Idle");

        //At the start of the fight I want eyes to spawn
        SpawnEye();





    }

    // Update is called once per frame
    void Update()
    {
        if (bossInstance != null)
        {
            // Get the current HP of the boss
            float currentHp = bossInstance.currentHealth;

            // If the boss hp dips below 20, it's time for phase 2
            if (currentHp <= 20 && !bossDead) // Check if bossDeath hasn't been called yet
            {
                phase2Enabled = true;

                // Check if boss HP is less than or equal to 0
                if (currentHp <= 0)
                {
                    // Call boss death function
                    bossDeath();
                    bossDead = true; // Set the flag to true to prevent multiple calls
                }
            }
        }


        if (!phase2Enabled)
        {
            // logic for every 20-second spawn of eyes
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
        } else if (phase2Enabled)
        {
            //once the health goes to a point where I want the chracter and boss to drop, I'm going to destroy specified prefabs/GOs
            Destroy(itemsToDestroy);

            bossAnimator.SetTrigger("SecondPhase");
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
        yield return new WaitForSeconds(.5f);
        Instantiate(eyeballPrefab, spawnPosition1.position, Quaternion.identity);
        yield return new WaitForSeconds(.5f);
        Instantiate(eyeballPrefab, spawnPosition2.position, Quaternion.identity);
        yield return new WaitForSeconds(.5f);
        Instantiate(eyeballPrefab, spawnPosition3.position, Quaternion.identity);
    }


    public void attack()
    {
        Vector2 pos = transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(pos, attackRange, attackMask);

        // Visualize the overlap circle
        Debug.DrawRay(pos, Vector2.up * attackRange, Color.green);
        Debug.DrawRay(pos, Vector2.right * attackRange, Color.green);
        Debug.DrawRay(pos, Vector2.left * attackRange, Color.green);
        Debug.DrawRay(pos, Vector2.down * attackRange, Color.green);

        // Check if any of the colliders are the player's collider
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {

                ScoreManager.instance.TakeScore(2);
                pc.StartCoroutine(pc.flashSprite());
                break;
            }
        }
    }


    public void bossDeath()
    {

        // Spawn the lever for the gate to leave
        // Instantiate(leverForDoor, transform.position, Quaternion.identity);
        StartCoroutine(DeathActions());
    }

    public IEnumerator DeathActions()
    {

        GameObject deathAnim = Instantiate(deathAnimPrefab, spawnDeathPos.transform.position, Quaternion.identity);

        yield return new WaitForSeconds(2.4f);

        bossAnimator.SetTrigger("Die");
        // If the code finds itself here, that means that the boss has died

        leverForDoor.SetActive(true);

        if (deathAnim != null)
        {
            Destroy(deathAnim);
        }
    }

}
