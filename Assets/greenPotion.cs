using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class greenPotion : MonoBehaviour
{
    public PlayerController pc;
    public GameObject greenPot;
    public GameObject playerEffect;
    public AudioSource ass;
    public static bool active;
    // Start is called before the first frame update
    void Start()
    {
        active = false;

        greenPot = gameObject;

        playerEffect = GameObject.FindGameObjectWithTag("greenPotionEffect");

        if (playerEffect != null)
        {
            //initially set the effect to false
            playerEffect.SetActive(false);
        }

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
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

        //change the duration of the time to longer
        Bullet.timeDuration = 8f;

        if (playerEffect != null)
        {
            //initially set the effect to false
            playerEffect.SetActive(true);
        }

        //for 8 seconds
        yield return new WaitForSeconds(8f);

        //change the color back to regular
        if (Bullet.ren != null)
        {
            Bullet.ren.color = Bullet.startingColor;
        }

        //set back to 4 seconds
        Bullet.timeDuration = 4f;

        if (playerEffect != null)
        {
            //initially set the effect to false
            playerEffect.SetActive(false);
        }

        active = false;

        Destroy(gameObject);
    }




}
