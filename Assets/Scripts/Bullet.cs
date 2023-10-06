using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject theBullet;
    public static float speed = 1.5f;
    public Rigidbody2D rb;
    public float damage = 3f;
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


    // Start is called before the first frame update
    void Start()
    {

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

    void Update()
    {
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
            rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;


            //if neither action 1 or 2, do this last resort action if P isnt clicked at all
            rb.isKinematic = false;

            //this is what makes it move
            rb.velocity = transform.right * speed;

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

            Debug.Log("Rounds are volatile: " + volatileRounds);
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
                radialExplosion.transform.localScale = new Vector3(3, 3, 3);
                float doubleDamage = damage * 2;
                enemy.TakeDamage(doubleDamage);
                Instantiate(radialExplosion, transform.position, transform.rotation);
                Destroy(gameObject);
            } else
            {
                enemy.TakeDamage(damage);
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
            enemy.TakeDamage(damage);
            //we don't want repetative damage on this
            Destroy(gameObject);
        }
        else if (hitInfo.gameObject.tag != "Enemy" && hitInfo.gameObject.tag != "Player")
        {
            //destroy the gameobject if it hits anything with a solid collider
            Destroy(gameObject);
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
        volatileRounds = true;
        rb.velocity = -transform.up * speed * 3 + new Vector3(0, -4, 0);

        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

        Weapon.canShoot = false; 
        yield return new WaitForSeconds(4f);

        volatileRounds = false;
        canHold = false;

        rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;

        PlayerController.released = false;

        Destroy(gameObject);
    }

}