using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

	public static ScoreManager instance;
	public Text scoreText;

    private GameManager theGM;

    public bool canChange = true;

    public GameObject soulSlot1;
    public GameObject soulSlot2;
    public GameObject soulSlot3;
    public GameObject soulSlot4;
    public GameObject soulSlot5;
    
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
        	instance = this;
        }
        Weapon.shotsLeft = PlayerPrefs.GetInt("CurrentScore");

        theGM = FindObjectOfType<GameManager>();
    }


    
    public void ChangeScore(int soulValue)
    {
	    if (Weapon.shotsLeft >= 5)
	    {
		    canChange = false;
		    Weapon.shotsLeft = 5; //tie back to five total in case of overflow
	    }
	    
	    if (canChange)
	    {
		    Weapon.shotsLeft += soulValue;
	    }

	    PlayerPrefs.SetInt("CurrentScore", Weapon.shotsLeft);
    	scoreText.text = "" + Weapon.shotsLeft.ToString();
        
    }

    public void TakeScore(int soulReduction) 
    {
	    if (Weapon.shotsLeft <= 0)
	    {
		    Weapon.shotsLeft = 0;
	    }
	    if (Weapon.shotsLeft >= 5)
	    {
		    
		    Weapon.shotsLeft = 5; //tie back to five total in case of overflow
	    }
	    
	    
	    Weapon.shotsLeft -= soulReduction;
        PlayerPrefs.SetInt("CurrentScore", Weapon.shotsLeft);
        scoreText.text = "" + Weapon.shotsLeft.ToString();
        //change score is true when souk is taken away
        canChange = true;

    }

    public void Update()
    {
	    switch (Weapon.shotsLeft)
	    {
		    case 0:
			    soulSlot1.SetActive(false);
			    soulSlot2.SetActive(false);
			    soulSlot3.SetActive(false);
			    soulSlot4.SetActive(false);
			    soulSlot5.SetActive(false);
			    break;
		    case 1:
			    soulSlot1.SetActive(true);
			    soulSlot2.SetActive(false);
			    soulSlot3.SetActive(false);
			    soulSlot4.SetActive(false);
			    soulSlot5.SetActive(false);
			    break;
		    case 2:
			    soulSlot1.SetActive(true);
			    soulSlot2.SetActive(true);
			    soulSlot3.SetActive(false);
			    soulSlot4.SetActive(false);
			    soulSlot5.SetActive(false);
			    break;
		    case 3:
			    soulSlot1.SetActive(true);
			    soulSlot2.SetActive(true);
			    soulSlot3.SetActive(true);
			    soulSlot4.SetActive(false);
			    soulSlot5.SetActive(false);
			    break;
		    case 4:
			    soulSlot1.SetActive(true);
			    soulSlot2.SetActive(true);
			    soulSlot3.SetActive(true);
			    soulSlot4.SetActive(true);
			    soulSlot5.SetActive(false);
			    break;
		    case 5:
			    soulSlot1.SetActive(true);
			    soulSlot2.SetActive(true);
			    soulSlot3.SetActive(true);
			    soulSlot4.SetActive(true);
			    soulSlot5.SetActive(true);
			    
			    break;
	    }
    }
    
}
