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

        //initially set the effect to false
        playerEffect.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
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

        Bullet.timeDuration = 8f;
        //also want to turn on our effect
        playerEffect.SetActive(true);

        //for 8 seconds
        yield return new WaitForSeconds(8f);

        Bullet.timeDuration = 4f;

        playerEffect.SetActive(false);

        active = false;

        Destroy(gameObject);
    }

}
