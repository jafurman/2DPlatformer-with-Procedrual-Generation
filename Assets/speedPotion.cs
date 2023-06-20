using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speedPotion : MonoBehaviour
{
    public PlayerController pc;
    public GameObject bluePotion;
    public GameObject playerEffect;
    public AudioSource ass;

    // Start is called before the first frame update
    void Start()
    {
        bluePotion = gameObject;

        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        playerEffect = GameObject.FindGameObjectWithTag("bluePotionEffect");

        if (playerEffect != null)
        {
            playerEffect.SetActive(false);
        }
        
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
        //we want to increaset the speed of the player movement and also increase the scythe rate
        pc.speed = 5;
        pc.ScytheRate = 6;

        //also want to turn on our effect
        if (playerEffect != null)
        {
            playerEffect.SetActive(true);
        }

        //for 8 seconds
        yield return new WaitForSeconds(8f);

        //then switch back to normal
        pc.speed = 3;
        pc.ScytheRate = 3.2f;

        if (playerEffect != null)
        {
            playerEffect.SetActive(false);
        }

        Destroy(gameObject);
    }

}
