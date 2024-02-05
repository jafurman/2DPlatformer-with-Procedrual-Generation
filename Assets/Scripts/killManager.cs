using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class killManager : MonoBehaviour
{
    public int killCounter;
    public int deathCounter;

    public Text killText;
    public Text deathText;

    public static killManager instance;
    // Start is called before the first frame update
    void Start()
    {
        killCounter = PlayerPrefs.GetInt("amountOfKills");
        deathCounter = PlayerPrefs.GetInt("amountOfDeaths");

        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        killText.text = " " + killCounter;
        deathText.text = " " + deathCounter;
    }

    public void addKillCounter()
    {
        killCounter++;
        PlayerPrefs.SetInt("amountOfKills", killCounter);
    }

    public void addDeathCounter()
    {
        deathCounter++;
        PlayerPrefs.SetInt("amountOfKills", deathCounter);
    }
}
