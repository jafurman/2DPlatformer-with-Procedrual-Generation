using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulScoreManager : MonoBehaviour
{
    public int soulSlots;
    public Text addedPoints;
    public Text change;
    public static float speedMultiplier;
    public static int latest;
    public static SoulScoreManager instance;

    public Button skill1;
    public Button skill2;
    public Button skill3;
    public Button skill4;
    public Button skill5;

    // Start is called before the first frame update
    void Start()
    {
        MainMenu.skillBuys = PlayerPrefs.GetInt("SkillBuys");

        if (PlayerPrefs.GetInt("SkillBuys") >= 5)
        {
            skill1.interactable = false;
            skill2.interactable = false;
            skill3.interactable = false;
            skill4.interactable = false;
            skill5.interactable = false;
        }

        latest = 0;
        speedMultiplier = 1;

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
    public void buyBulkySlot()
    {

        subtractPoints(1000);
        Weapon.shotsLeft++;
        int currentSlots = Weapon.shotsLeft;
        PlayerPrefs.SetInt("CurrentSoulSlots", currentSlots);
        PlayerPrefs.Save();
        Debug.Log("New soul slot added. Now: " + currentSlots);
    }

    public void buyAgilitySlot()
    {
            addPoints(1000);
            MainMenu.skillBuys++;
            speedMultiplier = speedMultiplier * 1.1f;
            PlayerPrefs.SetFloat("speedMultiplier", speedMultiplier);
            PlayerPrefs.SetInt("SkillBuys", MainMenu.skillBuys);
            PlayerPrefs.Save();

            switch (MainMenu.skillBuys)
            {
                case 1:
                    skill1.interactable = true;
                    skill2.interactable = false;
                    skill3.interactable = false;
                    skill4.interactable = false;
                    skill5.interactable = false;
                    break;
                case 2:
                    skill1.interactable = false;
                    skill2.interactable = true;
                    skill3.interactable = false;
                    skill4.interactable = false;
                    skill5.interactable = false;
                    break;
                case 3:
                    skill1.interactable = false;
                    skill2.interactable = false;
                    skill3.interactable = true;
                    skill4.interactable = false;
                    skill5.interactable = false;
                    break;
                case 4:
                    skill1.interactable = false;
                    skill2.interactable = false;
                    skill3.interactable = false;
                    skill4.interactable = true;
                    skill5.interactable = false;
                    break;
                case 5:
                    skill1.interactable = false;
                    skill2.interactable = false;
                    skill3.interactable = false;
                    skill4.interactable = false;
                    skill5.interactable = true;
                    break;
                default:
                    skill1.interactable = false;
                    skill2.interactable = false;
                    skill3.interactable = false;
                    skill4.interactable = false;
                    skill5.interactable = false;
                    break;

        
        }
        //scythe speed
    }

    public void buyMagicSlot()
    {
        subtractPoints(1000);
        Bullet.damage++;
    }

    public void subtractPointsForDev()
    {

        int currentScore = PlayerPrefs.GetInt("soulScore");
        currentScore -= 10;
        PlayerPrefs.SetInt("soulScore", currentScore);
        PlayerPrefs.Save();
        Debug.Log("Change: " + PlayerPrefs.GetInt("soulScore"));
        latest = 10;
    }


    //CONSIDER USING PLAYER PREFS FOR MAGIC, HP, and AGILITY slots ? 5 upgrades each? subtract player pref avaliable slots when clicking one?




}
