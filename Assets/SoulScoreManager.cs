using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulScoreManager : MonoBehaviour
{
    public int soulSlots;
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

    public void subtractPoints(int points)
    {

        int currentScore = PlayerPrefs.GetInt("soulScore");
        currentScore -= points;
        PlayerPrefs.SetInt("soulScore", currentScore);
        PlayerPrefs.Save();
        Debug.Log("Change: " + PlayerPrefs.GetInt("soulScore"));
        latest = points;
    }

    //tie these functions to buttons of this class instance in the HUD.. its a decent system I've set up.
    public void buyHpSlot()
    {

        subtractPoints(1000);
        Weapon.shotsLeft++;
        int currentSlots = Weapon.shotsLeft;
        PlayerPrefs.SetInt("CurrentSoulSlots", currentSlots);
        PlayerPrefs.Save();
        Debug.Log("New soul slot added. Now: " + currentSlots);
    }

    public void buyMobilitySlot()
    {

        subtractPoints(1000);
        PlayerController.speed += .5f;
        float currentSpeed = PlayerController.speed;
        //scythe speed
    }


    //CONSIDER USING PLAYER PREFS FOR MAGIC, HP, and AGILITY slots ? 5 upgrades each? subtract player pref avaliable slots when clicking one?

   


}
