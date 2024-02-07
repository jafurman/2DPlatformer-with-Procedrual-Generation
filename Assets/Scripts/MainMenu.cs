using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

	public string levelToLoad;
	public string PCGLEVELNAME;

	public int defaultLives;
	public int defaultScore;
	public int defaultSoulSlots = 5;
	public int defaultSoulScore = 0;
	public float defaultspeedMultiplier = 1;
	public static int skillBuys;
	private void Start() 
	{

			PlayerPrefs.SetInt("CurrentLives", defaultLives);
			PlayerPrefs.SetInt("CurrentScore", defaultScore);
			PlayerPrefs.SetInt("CurrentSoulSlots", defaultSoulSlots);
			PlayerPrefs.SetInt("soulScore", defaultSoulScore);
			PlayerPrefs.SetFloat("speedMultiplier", defaultspeedMultiplier);

			skillBuys = 1;
			PlayerPrefs.SetInt("SkillBuys", skillBuys);

	}
	public void playGame()
	{
		SceneManager.LoadScene(levelToLoad);
	}

	public void QuitGame()
	{
		Application.Quit(); 
	}

	public void PCGLevelStart()
    {
		SceneManager.LoadScene(PCGLEVELNAME);
    }
}
