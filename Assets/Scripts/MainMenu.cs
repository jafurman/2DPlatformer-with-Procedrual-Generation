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
	private void Start() 
	{
			PlayerPrefs.SetInt("CurrentLives", defaultLives);
			PlayerPrefs.SetInt("CurrentScore", defaultScore);
			PlayerPrefs.SetInt("CurrentSoulSlots", defaultSoulSlots);
			PlayerPrefs.SetInt("soulScore", defaultSoulScore);

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
