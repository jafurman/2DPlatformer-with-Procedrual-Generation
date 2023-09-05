using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class greenPotion : MonoBehaviour
{
    public PlayerController pc;
    public GameObject greenPot;
    public AudioSource ass;
    public static bool active;

    private int currentAmountofShots;
    // Start is called before the first frame update
    void Start()
    {
        active = false;

        greenPot = gameObject;

        

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //player gets current amount of shots left
        currentAmountofShots = Weapon.shotsLeft;

        //player 'picks' up the potion
        if (other.tag == "Player")
        {
            StartCoroutine(longerBulletTime());
            ass.Play();
            greenPot.GetComponent<Renderer>().enabled = false;
        }
    }

    IEnumerator longerBulletTime()
    {
        active = true;

        //get effects sprite
        SpriteRenderer sprite = PlayerController.magic.GetComponent<SpriteRenderer>();

        //Give the player 5 shots
        Weapon.shotsLeft = 5;

        //change the duration of the time to longer
        Bullet.timeDuration = 8f;

        //for 8 seconds
        yield return new WaitForSeconds(16f);

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


        //change the amount of shots back to amount at time of getting potion
        Weapon.shotsLeft = currentAmountofShots;

        //change the color back to regular
        if (Bullet.ren != null)
        {
            Bullet.ren.color = Bullet.startingColor;
        }

        //set back to 4 seconds
        Bullet.timeDuration = 4f;


        active = false;

        Destroy(gameObject);
    }




}
