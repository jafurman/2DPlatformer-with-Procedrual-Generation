using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speedPotion : MonoBehaviour
{
    public PlayerController pc;
    public GameObject bluePotion;
    public AudioSource ass;

    public static bool speedBoostOn;

    // Start is called before the first frame update
    void Start()
    {
        bluePotion = gameObject;

        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

       
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
       if (other.tag == "Player")
        {
            StartCoroutine(speedBoost());
            ass.Play();
            bluePotion.GetComponent<Renderer>().enabled = false;
        }
    }

    IEnumerator speedBoost()
    {
        //get effects sprite
        SpriteRenderer sprite = PlayerController.lightning.GetComponent<SpriteRenderer>();

        //we want to increaset the speed of the player movement and also increase the scythe rate


        pc.ScytheRate = 10;
        speedBoostOn = true;
        Bullet.speed = 3f;

        //for 8 seconds
        yield return new WaitForSeconds(18f);

        sprite.enabled = false;
        yield return new WaitForSeconds(.33f);
        sprite.enabled = true;
        yield return new WaitForSeconds(.33f);
        sprite.enabled = false;
        yield return new WaitForSeconds(.33f);
        sprite.enabled = true;
        yield return new WaitForSeconds(.33f);
        sprite.enabled = false;
        yield return new WaitForSeconds(.33f);
        sprite.enabled = true;
        yield return new WaitForSeconds(.33f);

        //then switch back to normal

        speedBoostOn = false;
        pc.ScytheRate = 3.2f;
        Bullet.speed = 1.5f;


        Destroy(gameObject);
    }

}
