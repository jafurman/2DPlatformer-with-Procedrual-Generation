using System.Collections;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public float maxHealth;
    [SerializeField] public float currentHealth;
    public GameObject deathEffect;
    public GameObject healthBar;
    public AudioSource deathSound;
    public int soulValue;
    private Animator theAnim;
    private bool isDead;
    public static bool isInvincible = false;
    public SoulScoreManager ssm;
    public GameObject otherBoss;
    public bool isBossOne, isBossTwo;
    public float boss1Hp, boss2Hp;
    public AudioSource se;
    public AudioClip clip;
    private void Start()
    {
        if (otherBoss != null)
        {
            currentHealth = maxHealth + otherBoss.GetComponent<Enemy>().maxHealth;
        } else 
        {
            currentHealth = maxHealth;
            boss1Hp = currentHealth;
            boss2Hp = currentHealth;
        }
        ssm = GameObject.FindGameObjectWithTag("soulScoreManager").GetComponent<SoulScoreManager>();
        theAnim = GetComponent<Animator>();

        if ( healthBar != null)
        {
            // Set the initial health bar size
            healthBar.transform.localScale = new Vector3(currentHealth / 10, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
        }

        
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (isBossOne)
            {
                boss1Hp = currentHealth;
            }
        if (isBossTwo)
            {
                boss2Hp = currentHealth;
            }
        
        if (gameObject.tag != "Enemy" || gameObject.tag != "Spooder")
        {
            theAnim.SetTrigger("takeDamage");
            if ((clip) != null && se != null)
                {
                    //once hit this sound will play
                    se.PlayOneShot(clip);
                }
        }
        StartCoroutine(flashSprite());


        if (healthBar != null)
        {
            // Calculate and set the new health bar size
            float proportionalDamage = damage / 10;
            float newHealthSize = healthBar.transform.localScale.x - proportionalDamage;
            

            healthBar.transform.localScale = new Vector3(newHealthSize, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
        }

        if (currentHealth <= 0)
        { 
            //turn off health when dead
            Destroy(healthBar);

            isDead = true;

            // Disable movement and collider
            Collider2D hitBox = GetComponent<Collider2D>();
            hitBox.enabled = false;

            SinusoidalMove sinMov = GetComponent<SinusoidalMove>();
            if (sinMov != null)
            {
                sinMov.enabled = false;
            }

            Spider spider = GetComponent<Spider>();
            if (spider != null)
            {
                spider.enabled = false;
            }

            ssm.addPoints(50);
            ScoreManager.instance.ChangeScore(soulValue);
            Die();
        }
    }

    private void Die()
    {
        killManager.instance.addKillCounter();

        if (deathSound != null)
        {
            deathSound.Play();
        }

        isDead = true;

        SinusoidalMove sinMov = GetComponent<SinusoidalMove>();
        if (sinMov != null)
        {
            sinMov.enabled = false;
        }

        Spider spider = GetComponent<Spider>();
        if (spider != null)
        {
            spider.enabled = false;
        }

        ScoreManager.instance.ChangeScore(soulValue);
        if (theAnim.parameters.Any(param => param.name == "die" && param.type == AnimatorControllerParameterType.Trigger))
        {
            // The "die" trigger exists, so set it and start the coroutine
            theAnim.SetTrigger("die");

            if (deathEffect != null)
            {
                Instantiate(deathEffect, transform.position, Quaternion.identity);
            }

            StartCoroutine(DestroyEnemy());

        }
        else
        {
            if (deathEffect != null)
            {
                Instantiate(deathEffect, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }

    private IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(theAnim.GetCurrentAnimatorStateInfo(0).length); // Wait for the length of the die animation state
        Destroy(gameObject); // Destroy the enemy game object
    }

    private IEnumerator StopAnim()
    { 
        yield return new WaitForSeconds(0.05f);
        theAnim.SetBool("Dead", isDead);
        Destroy(gameObject);
    }

    public IEnumerator flashSprite()
    {
        isInvincible = true;
        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();

        if (sprite != null)
        {
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

    }
}
