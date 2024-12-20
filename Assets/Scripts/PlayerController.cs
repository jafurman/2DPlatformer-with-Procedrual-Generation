using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// im back !! (I built a new PC)

public class PlayerController : MonoBehaviour
{
    public static float speed = 3f;
    public float jumpForce; //only need this if you want to control the height of the char jump
    public static bool canMove = true;
    private static bool inDialogueMode = false;
    public Rigidbody2D theRB2D; //IMPORTANT!! need a rigidbody component on the player

    public static bool grounded; //if player == grounded it can jump again
    public bool shoot; //if player == shooting then it can do the shoot animation

    public LayerMask
        whatIsGrd; //layerMask ground is so that we can place in sprites/things in hierarchy that we want to quantify as ground

    public Transform grdChecker;
    public Transform Player;

    public float grdCheckerRad;

    public float airTime;
    public float airTimeCounter; //control height jump

    public static bool ctrActive;

    public GameObject[] childObjs;
    public float shockForce;

    public static bool FacingRight = true;

    private Animator theAnimator;

    public GameManager gm; //If you want to add a title screen and end game screen
    private LivesManager theLM;
    private ScoreManager sm;

    public AudioSource stepSound;
    public AudioSource jumpSound;

    public Animator animator;

    public float meleeRange = 2f;

    private float meleeTimer = 0f;

    public static bool allowAttack = true;

    public AudioSource slice;
    public bool hasTeleported = false;
    public bool allowTeleport = true;
    public float teleportTimer = 0f;

    public AudioSource postTSE;
    public AudioSource chargeTSE;

    private bool isKnockedBack = false;

    public static bool isInvincible = false;

    public Transform ScythePoint;
    public float ScytheRange;
    public LayerMask enemyLayers;

    public float ScytheRate = 3f; //can Scythe once per second
    public float nextScytheTime = 0f;

    public AudioSource ScytheSoundOne;
    public AudioSource ScytheSoundTwo;

    public bool hasSwung;
    public int swingCount ;
    public static bool canJump = true;

    //Once the character swings there is a .36 second animation that plays
    public bool isSwinging = false;

    public static bool freezeOn;
    public static bool released;

    private float stopTime;

    public static bool forcedStop;

    public static GameObject wings;
    public static GameObject lightning;
    public static GameObject magic;

    public BarrelScript BarrelScriptInstance;

    public DisableManager dm;

    public static bool canUseM;
    public static bool canUseP;

    public static float cutSpeed = 1; //always should be 1 unless speed cut to 0

    public GameObject pReleaseAnim;
    public GameObject pReleaseAnimGO;

    public GameObject spawnSlashPrefab;

    public GameObject extraHopBoxPrefab;

    public SoulScoreManager ssm;

    public bool soundPlayed;

    private bool scoreTaken = false;
    public AudioSource se;
    public AudioClip clip;

    // Start is called before the first frame update
    private void Start()
    {
        dm = FindObjectOfType<DisableManager>();
        theLM = FindObjectOfType<LivesManager>();
        theRB2D = GetComponent<Rigidbody2D>(); //instance of rigidbody
        theAnimator = GetComponent<Animator>();

        airTimeCounter = airTime;

        ctrActive = true;
        swingCount = 0;

        //start of the game means you cannot hold the bullet yet
        Bullet.canHold = false;
        canUseM = true;
        canUseP = true;

        magic = GameObject.FindGameObjectWithTag("greenPotionEffect");
        wings = GameObject.FindGameObjectWithTag("yellowPotionEffect");
        lightning = GameObject.FindGameObjectWithTag("bluePotionEffect");

        wings.SetActive(false);
        lightning.SetActive(false);
        magic.SetActive(false);

        Weapon.canShoot = true;
    }
    

    // Update is called once per frame
    private void Update()
    {

        if (greenPotion.active)
        {
            magic.SetActive(true);
        } else
        {
            magic.SetActive(false);
        }

        if (flyPotion.active)
        {
            wings.SetActive(true);
        } else
        {
            wings.SetActive(false);
        }

        if (speedPotion.speedBoostOn)
        {
            lightning.SetActive(true);
        } else
        {
            lightning.SetActive(false);
        }


        if (Time.time >= nextScytheTime)
        {
            if (Input.GetMouseButtonDown(0) && Slider.isSliding == false)
            {
                float speedIncrease = PlayerPrefs.GetFloat("ScytheRateIncrease");
                nextScytheTime = Time.time + 1f / (ScytheRate - speedIncrease);

                if (!hasSwung)
                {
                    hasSwung = true;
                    Scythe();
                }
                else
                {
                    ScytheUp();
                    hasSwung = false; // reset the flag
                }
            }
        }

        if (canUseP == true)
        {
            //logic code for holding the bullets in kinematic if held
            if (Input.GetMouseButton(1))
            {
                //freeze the player in their current position fully
                theRB2D.gravityScale = .2f;
                cutSpeed = .2f;
                theRB2D.drag = 3f;

                freezeOn = true;
                released = false;

                //set the animation trigger to hold the soul
                animator.ResetTrigger("ReleaseSoul");
                animator.SetTrigger("HoldSoul");

                StartCoroutine(holdFalseTimer());

            }
            if (Input.GetMouseButtonUp(1))
            {
                //play sound here
                jumpSound.PlayOneShot(jumpSound.clip);
                
                StartCoroutine(destroyAnim());

                animator.SetTrigger("ReleaseSoul");
                animator.ResetTrigger("HoldSoul");
                //action 2
                freezeOn = false;
                released = true;
                StartCoroutine(turnReleasedFalse());
                //unfreeze position
                theRB2D.constraints = RigidbodyConstraints2D.FreezeRotation;
                if (!flyPotion.active)
                {
                    theRB2D.gravityScale = 1;
                }
                cutSpeed = 1;
                theRB2D.drag = 0;
                theRB2D.velocity = new Vector2(theRB2D.velocity.x, jumpForce + 2);
                StartCoroutine(dm.DisableP());


            }

        }
        

        
        //if input is left, can move, if input is right, can move
        if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
        {
            if (isKnockedBack)
            {
                canMove = false;
            }

        }

        //upwards and downwards animation.
        if (theRB2D.velocity.y < -0.05f && !freezeOn)
        {
            if ( isSwinging || released )
            {
                animator.SetBool("Downwards", false);
            } else
            {
                animator.SetBool("Downwards", true);
                animator.SetBool("Grounded", false);
            }

        }
        else if (!grounded)
        {
                animator.SetBool("Downwards", false);
        }

        if (canUseM == true)
        {
            if (Input.GetKey(KeyCode.LeftShift) && meleeTimer == 0f && !Slider.isSliding)
            {
                meleeTimer = 0.01f;
                animator.SetBool("HoldSlice", true);
            }

            if (!Input.GetKey(KeyCode.LeftShift) && allowAttack)
            {
                meleeTimer = 0;
                animator.SetBool("HoldSlice", false);
            }

            if (meleeTimer > 0)
            {
                meleeTimer += Time.deltaTime;
                if (meleeTimer >= 1.0f && allowAttack)
                {
                    PerformMeleeAttack();
                    slice.Play();
                    allowAttack = false;
                }

                if (meleeTimer >= 1.41f)
                {
                    animator.SetBool("HoldSlice", false);
                    allowAttack = true;
                    meleeTimer = 0f;

                }
            }
        } else if (canUseM == false)
        {
            animator.SetBool("HoldSlice", false);
        }
        

        //Teleportation code
        if (grounded && (Input.GetKeyDown(KeyCode.N) && !hasTeleported) && teleportTimer == 0f && Slider.isSliding == false)
        {
            teleportTimer = 0.01f;
            animator.SetBool("Teleport", true);
            chargeTSE.enabled = true;
            //hold down the key, teleport
        }

        if (Input.GetKeyUp(KeyCode.N))
        {
            teleportTimer = 0;
            animator.SetBool("Teleport", false);
            chargeTSE.enabled = false;
            //release the key, can teleport again to avoid 60 teleports per second
            hasTeleported = false;
        }

        if (teleportTimer > 0)
        {
            teleportTimer += Time.deltaTime;
            if (teleportTimer >= 1.1f && allowTeleport && !hasTeleported)
            {
                Teleport();
                //turn off charge SE when teleported to avoid overlap
                chargeTSE.enabled = false;
                postTSE.Play();
                hasTeleported = true;
                allowTeleport = true;
            }

            //however long the teleport animation needs to finish
            if (teleportTimer >= 1.8f)
            {
                animator.SetBool("Teleport", false);
                allowTeleport = true;
                teleportTimer = 0f;
            }
        }
    }


    //method that moves character forward by one space and swings the scythe
    void Scythe()
    {
        StartCoroutine(SpawnScytheSlash());

        float originalSpeed = speed;

        // I was so dog at writing code when I started this project so there is a coroutine for animation instead of directly in the animator tab
        StartCoroutine(AnimationDelay());

        //moves the playerover by the value specified
        Vector2 moveDirection = gameObject.transform.right;
        if (!FacingRight)
        {
            moveDirection *= -1;
        }

        //play audio
        ScytheSoundOne.PlayOneShot(ScytheSoundOne.clip);
        animator.SetTrigger("ScytheAttack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(ScythePoint.position, ScytheRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            // Check if the enemy has the "Enemy" component before accessing it
            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            if (enemyComponent != null)
            {
                enemyComponent.TakeDamage(1);
                StartCoroutine(lilBoxSpawn());

                ssm.addPoints(10);

            }

            if (enemy.tag == "Barrel")
            {
                ssm.addPoints(10);
                Vector3 enemyPosition = enemy.transform.position;
                BarrelScriptInstance.spawnBarrel(enemyPosition);
                Destroy(enemy.gameObject);
            }

            if (enemy.CompareTag("Enemy"))
            {
                // Assuming you have a specific LineAtPlayer instance related to the mage shot
                lineAtPlayer lap = enemy.GetComponent<lineAtPlayer>();
                if (lap != null)
                {
                    ssm.addPoints(100);
                    lap.MageShotSpeed = -lap.MageShotSpeed;
                    if ((clip) != null && se != null)
                    {
                        //once hit this sound will play
                        se.PlayOneShot(clip);
                    }
                }
            }
        }
    }


    //moves character up by one space and stops them from moving in the x direction for the remainder of the swing
    void ScytheUp()
    {
        StartCoroutine(SpawnScytheSlash());
        //stopTime
        stopTime = .3f;
        StartCoroutine(stopPlayerFor(stopTime));
       
        // because of my shitty animation setup skills I now have to coroutine when swinging
        StartCoroutine(AnimationDelay());

        //play audio
        ScytheSoundTwo.PlayOneShot(ScytheSoundTwo.clip);
        animator.SetTrigger("ScytheUpAttack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(ScythePoint.position, ScytheRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            // Check if the enemy has the "Enemy" component before accessing it
            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            if (enemyComponent != null)
            {
                enemyComponent.TakeDamage(1);
                StartCoroutine(lilBoxSpawn());

                ssm.addPoints(10);
            }

            if (enemy.tag == "Barrel")
            {
                ssm.addPoints(10);
                Vector3 enemyPosition = enemy.transform.position;
                BarrelScriptInstance.spawnBarrel(enemyPosition);
                Destroy(enemy.gameObject);
            }

            if (enemy.CompareTag("Enemy"))
            {
                // Assuming you have a specific LineAtPlayer instance related to the mage shot
                lineAtPlayer lap = enemy.GetComponent<lineAtPlayer>();
                if (lap != null)
                {
                    ssm.addPoints(100);
                    lap.MageShotSpeed = -lap.MageShotSpeed;
                    if ((clip) != null && se != null)
                    {
                        //once hit this sound will play
                        se.PlayOneShot(clip);
                    }
                }
            }

        }
    }

    private void OnDrawGizmosSelected()
    {
        if (ScythePoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(ScythePoint.position, ScytheRange);
    }

    void Teleport()
    {
        
        Vector2 raycastDirection = transform.right;
        Vector2 raycastStart = (Vector2)transform.position + raycastDirection * 0.5f;
        RaycastHit2D hit = Physics2D.Raycast(raycastStart, raycastDirection);

        if (hit.collider != null)
        {
            float distance = hit.distance;

            if (distance > 0)
            {
                if (distance > 3)
                {
                    transform.position = transform.position + (Vector3)(raycastDirection * (3));
                    Debug.Log("The distance is greater than 3: " + distance);
                }
                else
                {
                    transform.position = transform.position + (Vector3)(raycastDirection * (distance + 0.2f));
                    Debug.Log("The distance is less than or equal to 3: " + distance);
                }
            }
            else
            {
                Debug.Log("The distance is 0");
            }
        }
        else
        {
            Debug.Log("The raycast did not hit anything");
        }
    }

    private void PerformMeleeAttack()
    {
        // take away a soul for its use, but does more damage damage
        ScoreManager.instance.TakeScore(1);

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, meleeRange);

            foreach (Collider2D hit in hits)
            {
            if (hit.tag == "Barrel")
            {
                Vector3 enemyPosition = hit.transform.position;
                BarrelScriptInstance.spawnBarrel(enemyPosition);
                ssm.addPoints(10);
                Destroy(hit.gameObject);
            }
            //ideally it should only be enemy
            //but I am a shit programmer so you get Spooder too
            if (hit.CompareTag("Enemy") || hit.CompareTag("Spooder") || hit.CompareTag("Warden"))
                {
                    Enemy enemy = hit.GetComponent<Enemy>();
                    Spider spider = hit.GetComponent<Spider>();

                    if (enemy != null)
                    {
                        enemy.TakeDamage(3);

                        ssm.addPoints(10);

                }
                }
            if (hit.CompareTag("Enemy"))
            {
                // Assuming you have a specific LineAtPlayer instance related to the mage shot
                lineAtPlayer lap = hit.GetComponent<lineAtPlayer>();
                if (lap != null)
                {
                    ssm.addPoints(100);
                    lap.MageShotSpeed = -lap.MageShotSpeed;
                    if ((clip) != null && se != null)
                    {
                        //once hit this sound will play
                        se.PlayOneShot(clip);
                    }
                }
            }
        }
        StartCoroutine(dm.DisableM());
    }

    private void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(grdChecker.position, grdCheckerRad, whatIsGrd); //controlling jump height

        if (ctrActive)
        {
            MovePlayer();
            Jump();
            
        }
    }

    void MovePlayer()
    {
        if (isSwinging)
        {
            speed = 1.5f;
        } else if (!isSwinging && !speedPotion.speedBoostOn)
        {
            float multiplier = PlayerPrefs.GetFloat("speedMultiplier");
            speed = 3 * multiplier;
        } else if (!isSwinging && speedPotion.speedBoostOn)
        {
            speed = 4f;
        }
            if (canMove && !inDialogueMode)
            {
                theRB2D.velocity =
                    new Vector2(Input.GetAxisRaw("Horizontal") * speed * cutSpeed,
                        theRB2D.velocity.y); //Vector 2 is essentially a unity movement plaftorm on a x,y,z plane. 
                                             //important to mention that "horizontal" keys on getAxisRaw apply to left and right arrows and 'a' and 'd' keys.
                theAnimator.SetFloat("Speed", Mathf.Abs(theRB2D.velocity.x)); //animation code
            }

            if (theRB2D.velocity.x > 0 &&
                !FacingRight) //more sprite animation code so that the character can turn around when going opposing direction 
            {
                // transform.localScale = new Vector2(1f, 1f);
                Flip();
            }
            else if (theRB2D.velocity.x < 0 && FacingRight)
            {
                //transform.localScale = new Vector2(-1f, 1f);
                Flip();
            }
        }
        

    private void Flip()
    {
        FacingRight = !FacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    public void Jump()
    {
        if (grounded && !inDialogueMode && canJump && !isSwinging)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                theRB2D.velocity = new Vector2(theRB2D.velocity.x, jumpForce);
                if (!soundPlayed)
                {
                    jumpSound.PlayOneShot(jumpSound.clip);
                    soundPlayed = true;
                }
            }
        }

        if ((Input.GetKey(KeyCode.W)) && !inDialogueMode)
        {
            if (airTimeCounter > 0)
            {
                theRB2D.velocity = new Vector2(theRB2D.velocity.x, jumpForce);
                airTimeCounter -= Time.deltaTime;
            }
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            airTimeCounter = 0;
        }

        if (grounded)
        {
            airTimeCounter = airTime;
            soundPlayed = false; // Reset the soundPlayed variable when grounded
        }

        theAnimator.SetBool("Grounded", grounded);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.tag == "Enemy" && !isKnockedBack) || (other.gameObject.tag == "Spooder" && !isKnockedBack))
        {
            if (Mathf.Abs(transform.position.z - other.transform.position.z) > 0.01f)
            {
                // ignore any collisions between the two colliders
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other);
            }

            if (!scoreTaken)
            {
                ScoreManager.instance.TakeScore(1);
                scoreTaken = true;
            }
            StartCoroutine(flashSprite());
    
            if ((clip) != null && se != null)
                {
                    //once hit this sound will play
                    se.PlayOneShot(clip);
                }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Spooder")
        {
            scoreTaken = false;
        }
    }


    public IEnumerator flashSprite()
    {
        isInvincible = true;
        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
        sprite.enabled = false;
        yield return new WaitForSeconds(.2f);
        sprite.enabled = true;
        yield return new WaitForSeconds(.2f);
        sprite.enabled = false;
        yield return new WaitForSeconds(.2f);
        sprite.enabled = true;
        yield return new WaitForSeconds(.2f);
        sprite.enabled = false;
        yield return new WaitForSeconds(.2f);
        sprite.enabled = true;
        isInvincible = false;
    }

    public IEnumerator AnimationDelay()
    {
        // I use this boolean delay so that the swing in air plays as falling
        isSwinging = true;
        yield return new WaitForSeconds(.33f);
        isSwinging = false;
    }

    public IEnumerator turnReleasedFalse()
    {
        released = true;

        yield return new WaitForSeconds(1f);

        released = false;
    }


    //stop the rigidbody from moving and start the coroutine for kinematic to reset
    private IEnumerator stopPlayerFor(float time)
    {
        //change speed
        

        yield return new WaitForSeconds(time);

       
        theRB2D.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public IEnumerator holdFalseTimer()
    {
        freezeOn = true;

        yield return new WaitForSeconds(4f);

        animator.ResetTrigger("HoldSoul");
        animator.SetTrigger("ReleaseSoul");
        //action 2
        freezeOn = false;
        released = true;
        StartCoroutine(turnReleasedFalse());
        //unfreeze position
        if (!DisabledManager.disabledP.activeSelf)
        {
            theRB2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            theRB2D.velocity = new Vector2(theRB2D.velocity.x, jumpForce + 2);
            StartCoroutine(dm.DisableP());
        }

    }

    private IEnumerator destroyAnim()
    {
        Vector3 playerPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y);
        playerPos.y -= .05f;
        playerPos.x += .15f;
        pReleaseAnimGO = Instantiate(pReleaseAnim, playerPos, Quaternion.identity);

        yield return new WaitForSeconds(.35f);

        Destroy(pReleaseAnimGO);
    }

    public IEnumerator SpawnScytheSlash()
    {
        Vector3 scythePoint = GameObject.FindGameObjectWithTag("Scythe").transform.position;
        GameObject spawnSlash = Instantiate(spawnSlashPrefab, scythePoint, Quaternion.identity);
        yield return new WaitForSeconds(.5f);
        Destroy(spawnSlash);
    }


    public IEnumerator lilBoxSpawn()
    {
        Vector3 lilBoxSpot = gameObject.transform.position;
        lilBoxSpot.y -= .4f;
        GameObject box = Instantiate(extraHopBoxPrefab, lilBoxSpot, Quaternion.identity);
        yield return new WaitForSeconds(.2f);
        Destroy(box);
    }




}
