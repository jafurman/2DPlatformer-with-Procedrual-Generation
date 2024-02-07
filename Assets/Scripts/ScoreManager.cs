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
	public GameObject soulSlot11;
	public GameObject soulSlot22;
	public GameObject soulSlot33;
	public GameObject soulSlot44;
	public GameObject soulSlot55;

	private Vector3 playerPos;

	public GameObject minusOnePrefab;

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
	    if (Weapon.shotsLeft >= 5 + PlayerPrefs.GetInt("ExtraSoulSlots"))
	    {
		    canChange = false;
		    Weapon.shotsLeft = 5 + PlayerPrefs.GetInt("ExtraSoulSlots"); //tie back to five + extra soul slots total in case of overflow
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
			LivesManager.instance.TakeLife();
			Weapon.shotsLeft = 0;
		}
		else
		{
			Weapon.shotsLeft -= soulReduction;
			StartCoroutine(spawnMinusOneAnim());

		}
		PlayerPrefs.SetInt("CurrentScore", Weapon.shotsLeft);
		scoreText.text = Weapon.shotsLeft.ToString();
		canChange = true;
	}


	public void Update()
    {
		playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

        switch (Weapon.shotsLeft)
        {
            case 0:
                soulSlot1.SetActive(false);
                soulSlot2.SetActive(false);
                soulSlot3.SetActive(false);
                soulSlot4.SetActive(false);
                soulSlot5.SetActive(false);
                soulSlot11.SetActive(false);
                soulSlot22.SetActive(false);
                soulSlot33.SetActive(false);
                soulSlot44.SetActive(false);
                soulSlot55.SetActive(false);
                break;
            case 1:
                soulSlot1.SetActive(true);
                soulSlot2.SetActive(false);
                soulSlot3.SetActive(false);
                soulSlot4.SetActive(false);
                soulSlot5.SetActive(false);
                soulSlot11.SetActive(false);
                soulSlot22.SetActive(false);
                soulSlot33.SetActive(false);
                soulSlot44.SetActive(false);
                soulSlot55.SetActive(false);
                break;
            case 2:
                soulSlot1.SetActive(true);
                soulSlot2.SetActive(true);
                soulSlot3.SetActive(false);
                soulSlot4.SetActive(false);
                soulSlot5.SetActive(false);
                soulSlot11.SetActive(false);
                soulSlot22.SetActive(false);
                soulSlot33.SetActive(false);
                soulSlot44.SetActive(false);
                soulSlot55.SetActive(false);
                break;
            case 3:
                soulSlot1.SetActive(true);
                soulSlot2.SetActive(true);
                soulSlot3.SetActive(true);
                soulSlot4.SetActive(false);
                soulSlot5.SetActive(false);
                soulSlot11.SetActive(false);
                soulSlot22.SetActive(false);
                soulSlot33.SetActive(false);
                soulSlot44.SetActive(false);
                soulSlot55.SetActive(false);
                break;
            case 4:
                soulSlot1.SetActive(true);
                soulSlot2.SetActive(true);
                soulSlot3.SetActive(true);
                soulSlot4.SetActive(true);
                soulSlot5.SetActive(false);
                soulSlot11.SetActive(false);
                soulSlot22.SetActive(false);
                soulSlot33.SetActive(false);
                soulSlot44.SetActive(false);
                soulSlot55.SetActive(false);
                break;
            case 5:
                soulSlot1.SetActive(true);
                soulSlot2.SetActive(true);
                soulSlot3.SetActive(true);
                soulSlot4.SetActive(true);
                soulSlot5.SetActive(true);
                soulSlot11.SetActive(false);
                soulSlot22.SetActive(false);
                soulSlot33.SetActive(false);
                soulSlot44.SetActive(false);
                soulSlot55.SetActive(false);
                break;
            case 6:
                soulSlot1.SetActive(true);
                soulSlot2.SetActive(true);
                soulSlot3.SetActive(true);
                soulSlot4.SetActive(true);
                soulSlot5.SetActive(true);
                soulSlot11.SetActive(true);
                soulSlot22.SetActive(false);
                soulSlot33.SetActive(false);
                soulSlot44.SetActive(false);
                soulSlot55.SetActive(false);
                break;
            case 7:
                soulSlot1.SetActive(true);
                soulSlot2.SetActive(true);
                soulSlot3.SetActive(true);
                soulSlot4.SetActive(true);
                soulSlot5.SetActive(true);
                soulSlot11.SetActive(true);
                soulSlot22.SetActive(true);
                soulSlot33.SetActive(false);
                soulSlot44.SetActive(false);
                soulSlot55.SetActive(false);
                break;
            case 8:
                soulSlot1.SetActive(true);
                soulSlot2.SetActive(true);
                soulSlot3.SetActive(true);
                soulSlot4.SetActive(true);
                soulSlot5.SetActive(true);
                soulSlot11.SetActive(true);
                soulSlot22.SetActive(true);
                soulSlot33.SetActive(true);
                soulSlot44.SetActive(false);
                soulSlot55.SetActive(false);
                break;
            case 9:
                soulSlot1.SetActive(true);
                soulSlot2.SetActive(true);
                soulSlot3.SetActive(true);
                soulSlot4.SetActive(true);
                soulSlot5.SetActive(true);
                soulSlot11.SetActive(true);
                soulSlot22.SetActive(true);
                soulSlot33.SetActive(true);
                soulSlot44.SetActive(true);
                soulSlot55.SetActive(false);
                break;
            case 10:
                soulSlot1.SetActive(true);
                soulSlot2.SetActive(true);
                soulSlot3.SetActive(true);
                soulSlot4.SetActive(true);
                soulSlot5.SetActive(true);
                soulSlot11.SetActive(true);
                soulSlot22.SetActive(true);
                soulSlot33.SetActive(true);
                soulSlot44.SetActive(true);
                soulSlot55.SetActive(true);
                break;
        }
        }

	private IEnumerator spawnMinusOneAnim()
	{
		Debug.Log("Calling function");
		playerPos.y += .5f;
		GameObject minusOneGO = Instantiate(minusOnePrefab, playerPos, Quaternion.identity);

		yield return new WaitForSeconds(1f);

		Destroy(minusOneGO);
	}

}
