using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulScoreManager : MonoBehaviour
{
    public Text addedPoints;
    public Text change;

    public static int latest;
    public static SoulScoreManager instance;
    // Start is called before the first frame update
    void Start()
    {
        latest = 0;
    }

    // Update is called once per frame
    void Update()
    {
        int currentScore = PlayerPrefs.GetInt("soulScore");
        addedPoints.text = " " + currentScore;

        change.text = " + " + latest;
    }


    public void addPoints(int points)
    {
        int currentScore = PlayerPrefs.GetInt("soulScore");
        currentScore += points;
        PlayerPrefs.SetInt("soulScore", currentScore);
        PlayerPrefs.Save();
        Debug.Log("Change: " + PlayerPrefs.GetInt("soulScore"));
        latest = points;
        
    }
}
