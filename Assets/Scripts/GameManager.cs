using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string mainMenu;

	public PlayerController thePlayer;
	public Pumkin thePumpkin;
    public static Vector2 playerStart;

	public GameObject victoryScreen;
	public GameObject gameOverScreen;

    public GameObject spawnEffect;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerStart = thePlayer.transform.position;
        if (!PlayerPrefs.HasKey("soulScore"))
        {
            PlayerPrefs.SetInt("soulScore", 0);
            PlayerPrefs.Save();
        }
        Debug.Log("Start: "+PlayerPrefs.GetInt("soulScore"));
        //spawnEffect = GameObject.FindGameObjectWithTag("SpawnEffect");
    }


    /*
    public void Victory() 
    {
    		victoryScreen.SetActive(true);
    		thePlayer.gameObject.SetActive(false);
            thePumpkin.gameObject.SetActive(false);
            
    }
    */

    public void GameOver() 
    {
          if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
            thePlayer.gameObject.SetActive(false);
            StartCoroutine("GameReset");
        } else
        {
            thePlayer.gameObject.SetActive(false);
            StartCoroutine("GameReset");
        }


    }

    IEnumerator GameReset()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(mainMenu);
        thePlayer.gameObject.SetActive(true);
        thePumpkin.gameObject.SetActive(true);
        PlayerController.canMove = true;
    }

    public void Reset() 
    {
    		gameOverScreen.SetActive(false);
    		thePlayer.gameObject.SetActive(false);

            //If the player is hit or runs into anything there will be a delay before they respawn
            StartCoroutine(respawnDelay());
    }


    IEnumerator respawnDelay()
    {
        SpriteRenderer playerSprite = thePlayer.gameObject.GetComponent<SpriteRenderer>();
        playerSprite.enabled = true;

        yield return new WaitForSeconds(1.5f);
        thePlayer.transform.position = playerStart;
        yield return new WaitForSeconds(.5f);

        if (spawnEffect != null)
        {
            GameObject effectInstance = Instantiate(spawnEffect, thePlayer.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1.5f);
            Destroy(effectInstance);
        }

        thePlayer.gameObject.SetActive(true);
        Slider.HaveNotJumped = true;
        PlayerController.speed = 3f;
        thePlayer.theRB2D.gravityScale = 1f;
        PlayerController.cutSpeed = 1;
        thePlayer.theRB2D.drag = 0f;
        PlayerController.freezeOn = false;
        PlayerController.allowAttack = true;
        Slider.isSliding = false;

        GameObject slideIndicator = GameObject.FindGameObjectWithTag("slideIndicator");

        if (slideIndicator != null)
        {
            slideIndicator.SetActive(false);
        }
    }
    
    }
