using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pumkin : MonoBehaviour
{
	public GameManager gm;
    public GameObject victoryScreen;
    public GameObject player;
    public string nextLevel;


    public void Start()
    {
        victoryScreen.SetActive(false);
    }

    public void OnTriggerStay2D(Collider2D other)
    {
    	if (other.gameObject.tag == "Player")
    	{

            victoryScreen.SetActive(true);
            if (PlayerController.FacingRight && Input.GetKeyDown(KeyCode.W))
            {
                SceneManager.LoadScene(nextLevel);
            }
        }

            
        
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        victoryScreen.SetActive(false);
    }
}
