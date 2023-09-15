using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class killManager : MonoBehaviour
{
    public int killCounter;

    public Text killText;

    public static killManager instance;
    // Start is called before the first frame update
    void Start()
    {
        killCounter = PlayerPrefs.GetInt("amountOfKills");

        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        killText.text = " " + killCounter;
    }

    public void addKillCounter()
    {
        killCounter++;
        PlayerPrefs.SetInt("amountOfKills", killCounter);
    }
}
