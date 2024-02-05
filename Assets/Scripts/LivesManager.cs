using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesManager : MonoBehaviour
{

	//public int defaultLives;
	public int livesCounter;

	public Text livesText;

	private GameManager theGM;

    public int soulReduction = 0;

    public static LivesManager instance;

    private Vector3 playerPos;

    public GameObject minusOnePrefab;

    // Start is called before the first frame update
    void Start()
    {

        if (instance == null)
        {
            instance = this;
        }
        //livesCounter = defaultLives;
        livesCounter = PlayerPrefs.GetInt("CurrentLives");

        theGM = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        livesText.text = " " + livesCounter;

        if (livesCounter < 1)
        {
        	theGM.GameOver();
        }

        playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

    }

    public void TakeLife()
    {
        if (PlayerController.isInvincible == false)
        {
            livesCounter--;
            StartCoroutine(spawnMinusOneAnim());
            PlayerPrefs.SetInt("CurrentLives", livesCounter);

            killManager.instance.addDeathCounter();

            if (Weapon.shotsLeft > 0)
            {
                ScoreManager.instance.TakeScore(soulReduction);
            }
        }
    }

    public void AddLife()
    {
    	livesCounter++;
    	PlayerPrefs.SetInt("CurrentLives", livesCounter);
    }

    private IEnumerator spawnMinusOneAnim()
    {
        playerPos.y += .5f;
        GameObject minusOneGO = Instantiate(minusOnePrefab, playerPos, Quaternion.identity);

        yield return new WaitForSeconds(1f);

        Destroy(minusOneGO);
    }


}
