using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject theBullet;
    public static float speed = 1.5f;
    public Rigidbody2D rb;
    public static float damage = 1f;
    public GameObject impactEffect;

    public GameObject player; //for the player's info

    public AudioSource playSound;
    private float scaleDecreaseRate = 0.05f; // rate at which to decrease the scale of the game object

    public static float timeDuration = 4;

    public static SpriteRenderer ren;
    public static Color startingColor;

    public static bool canHold = false;

    public bool volatileRounds;

    public LayerMask enemyLayers;

    public GameObject spawnTailPrefab;

    public static bool tracking = false;

    private GameObject[] enemies;

    public SoulScoreManager ssm;
    // Start is called before the first frame update
    void Start()
    {
        ssm = GameObject.FindGameObjectWithTag("soulScoreManager").GetComponent<SoulScoreManager>();

        GameObject[] wardens = GameObject.FindGameObjectsWithTag("Warden");
        GameObject[] spooders = GameObject.FindGameObjectsWithTag("Spooder");
        GameObject[] genericEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Combine the arrays into a single array
        enemies = wardens.Concat(spooders).Concat(genericEnemies).ToArray();

        // gets the start color so I can make changes to the color and revert back
        ren = GetComponent<SpriteRenderer>();

        //this conditional checks if the green potion is on. If it is then this potion will stay green throughout its entire life 
        if (greenPotion.active)
        {
            startingColor = Color.green;
        }
        else
        {
            startingColor = ren.color;
        }

        //make canHold true
        canHold = true;
        volatileRounds = false;

        //because we want this bullet to have a lifetime we create this coroutine to kill it after specified time
        StartCoroutine(timeStop());


    }

    GameObject FindNearestEnemy()
    {
        GameObject nearestEnemy = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }


    void Update()
    {

        //Get the player prefs for the soul upgrades
        float factor = PlayerPrefs.GetFloat("TrackingAndSinLevel");
        if (factor != 0)
        {
            tracking = true;
        }

        //HOLDING
        if (PlayerController.freezeOn && !PlayerController.released)
        {
            //action 1 to happen
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
        }

        //RELEASING
        if (!PlayerController.freezeOn && PlayerController.released)
        {
            //flip the released variable back to false
            StartCoroutine(deRelease());
        }
        if (!PlayerController.freezeOn && !PlayerController.released)
        {
            normalSoulMovement();
        }



    }

    public void OnTriggerEnter2D(Collider2D hitInfo)  //when anything happens when bullet hits
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();

        if (enemy != null)
        {
            

            if (volatileRounds)
            {
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(gameObject.transform.position, 3, enemyLayers);
                foreach (Collider2D surroundingEnemies in hitEnemies)
                {
                    // Check if the enemy has the "Enemy" component before accessing it
                    Enemy enemyComponent = surroundingEnemies.GetComponent<Enemy>();
                    if (enemyComponent != null)
                    {
                        enemyComponent.TakeDamage(1);
                    }
                }
                GameObject radialExplosion = impactEffect;
                float doubleDamage = damage * 4;
                enemy.TakeDamage(doubleDamage);
                ssm.addPoints(50);
                Instantiate(radialExplosion, transform.position, transform.rotation);
                Destroy(gameObject);
            } else
            {
                int sda = PlayerPrefs.GetInt("soulDamageAddition");
                int combo = (int)(damage + sda);
                Debug.Log("Enemy is currently taking this much damage: " + combo);
                enemy.TakeDamage(combo);

                ssm.addPoints(10);
                //destroy the gameobject and instantiate impactEffect if it hits the enemy
                Instantiate(impactEffect, transform.position, transform.rotation);
                Destroy(gameObject);
            }

        }


        playSound.Play();


    }


    public void OnCollisionEnter2D(Collision2D hitInfo)
    {


        Enemy enemy = hitInfo.collider.GetComponent<Enemy>();

        if (hitInfo.gameObject.tag == "Player")
        {
            player = hitInfo.gameObject;
        }
        else if (hitInfo.gameObject.tag == "Enemy")
        {
            int sda = PlayerPrefs.GetInt("soulDamageAddition");
            int combo = (int)(damage + sda);
            Debug.Log("Enemy is currently taking this much damage: " + combo);
            enemy.TakeDamage(combo);
            ssm.addPoints(10);
            //we don't want repetative damage on this
            Destroy(gameObject);
        }
        else if (hitInfo.gameObject.tag != "Enemy" && hitInfo.gameObject.tag != "Player")
        {
            //destroy the gameobject if it hits anything with a solid collider
            Destroy(gameObject);
            //code for sounds of it running into things
        }


        Instantiate(impactEffect, transform.position, transform.rotation);


    }


    //don't know why this is needed
    public void OnCollisionExit2D(Collision2D coal)
    {
        if (coal.gameObject.tag == "Player")
        {
            player = null;
        }
    }

    public void normalSoulMovement()
    {
        if (tracking)
        {
            StartCoroutine(delayTracking());
        } else
        {
            rb.velocity = transform.right * speed;
            rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }

        //if neither action 1 or 2, do this last resort action if P isnt clicked at all
        rb.isKinematic = false;


        if (player != null)
        {
            if (PlayerController.FacingRight == true)
            {
                player.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            }
            else if (PlayerController.FacingRight == false)
            {
                player.transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
            }
        }

        // decrease the X scale of the game object
        transform.localScale -= new Vector3(scaleDecreaseRate * Time.deltaTime, 0f, 0f);


        if (greenPotion.active == true)
        {
            //change the color of the soul shot
            Bullet.ren.color = Color.green;
        }
        else
        {
            Bullet.ren.color = startingColor;
        }
    }

    public void trackEnemy(GameObject ne)
    {
        Debug.Log("Function called" + ne);
        if (tracking && ne != null)
        {
            rb.constraints = RigidbodyConstraints2D.None;

            Vector3 direction = (ne.transform.position - transform.position).normalized;
            speed += PlayerPrefs.GetInt("TrackingAndSinLevel");
            rb.velocity = direction * speed;

        }
    }

    //time until the bullet stops
    IEnumerator timeStop()
    {
        yield return new WaitForSeconds(timeDuration);

        //Make the cool effect just in case
        Instantiate(impactEffect, transform.position, transform.rotation);

        //after all is done make canHold back to False
        canHold = false;

        Destroy(gameObject);
    }

    IEnumerator deRelease()
    {
        Vector3 spawnPos = gameObject.transform.position;
        spawnPos.x += .2f;
        spawnPos.y += .1f;
        GameObject spawnTailGO = Instantiate(spawnTailPrefab, spawnPos, Quaternion.identity);

        volatileRounds = true;
        rb.velocity = -transform.up * speed * 3 + new Vector3(0, -4, 0);

        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

        Weapon.canShoot = false;

        yield return new WaitForSeconds(.4f);
        Destroy(spawnTailGO);

        yield return new WaitForSeconds(3.4f);

        volatileRounds = false;
        canHold = false;

        rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;

        PlayerController.released = false;

        Destroy(gameObject);
    }

    public IEnumerator delayTracking()
    {
        rb.velocity = transform.right * speed;
        rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        yield return new WaitForSeconds(2f);

        Bullet.ren.color = Color.yellow;
        GameObject ne = FindNearestEnemy();
        trackEnemy(ne);
    }

}