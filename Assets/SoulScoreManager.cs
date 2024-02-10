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

    public Button skill11, skill22, skill33, skill44, skill55, skill111, skill222, skill333, skill444, skill555;

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
        addPoints(50000000);

    }

    // Update is called once per frame
    void Update()
    {
        int currentScore = PlayerPrefs.GetInt("soulScore");
        addedPoints.text = " " + currentScore;

        change.text = " : " + latest;
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

        skillSlots();
    }

    
    //tie these functions to buttons of this class instance in the HUD.. its a decent system I've set up.
    //all three are to be used as separate buttons that are pressed after selecting their character build.
    
    public void buyBulkySlot()
    {
        int currentScore = PlayerPrefs.GetInt("soulScore");
        if ((currentScore - 1000) <= 0)
        {
            Debug.Log("You don't hav enof mony");
        }
        else
        {
            subtractPoints(1000);
            MainMenu.skillBuys++;
            PlayerPrefs.SetInt("SkillBuys", MainMenu.skillBuys);

            int ess = PlayerPrefs.GetInt("ExtraSoulSlots");
            ess++;
            PlayerPrefs.SetInt("ExtraSoulSlots", ess);
            PlayerPrefs.Save();
            Debug.Log("New soul slot added. Now: " + (5 + ess));
            //make sure that the extra slots work

            PlayerPrefs.Save();

            skillSlots();
        }

    }

    public void buyAgilitySlot()
    {
        int currentScore = PlayerPrefs.GetInt("soulScore");
        if ((currentScore - 1000) <= 0)
        {
            Debug.Log("You don't hav enof mony");
        }
        else
        {
            subtractPoints(1000);
            MainMenu.skillBuys++;
            PlayerPrefs.SetInt("SkillBuys", MainMenu.skillBuys);

            speedMultiplier = speedMultiplier * 1.05f;
            PlayerPrefs.SetFloat("speedMultiplier", speedMultiplier);

            float slideJumpMultiplier = PlayerPrefs.GetFloat("slideJumpMultiplier");
            slideJumpMultiplier = slideJumpMultiplier + .2f;
            PlayerPrefs.SetFloat("slideJumpMultiplier", slideJumpMultiplier);

            float decrementThis = PlayerPrefs.GetFloat("ScytheRateIncrease");
            decrementThis = decrementThis - .25f;
            PlayerPrefs.SetFloat("ScytheRateIncrease", decrementThis);

            PlayerPrefs.Save();
            //scythe speed

            skillSlots();
        }
        
    }

    public void buyMagicSlot()
    {
        int currentScore = PlayerPrefs.GetInt("soulScore");
        if ((currentScore - 1000) <= 0)
        {
            Debug.Log("You don't hav enof mony");
        }
        else
        {
            subtractPoints(1000);
            MainMenu.skillBuys++;
            PlayerPrefs.SetInt("SkillBuys", MainMenu.skillBuys);


            int sda = PlayerPrefs.GetInt("soulDamageAddition");
            sda++;
            PlayerPrefs.SetInt("soulDamageAddition", sda);

            float tasLevel = PlayerPrefs.GetInt("TrackingAndSinLevel");
            tasLevel += .25f;
            PlayerPrefs.SetFloat("TrackingAndSinLevel", tasLevel);
            Bullet.speed += tasLevel;
            PlayerPrefs.Save();


            Debug.Log("Soul now does " + (Bullet.damage + sda) + " damage");

            skillSlots();
        }
        
    }

    private void skillSlots()
    {
        int pt = PlayerPrefs.GetInt("PlayerType");
        if (pt == 1)
        {
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
        }

        if (pt == 2)
        {
            switch (MainMenu.skillBuys)
            {
                case 1:
                    skill11.interactable = true;
                    skill22.interactable = false;
                    skill33.interactable = false;
                    skill44.interactable = false;
                    skill55.interactable = false;
                    break;
                case 2:
                    skill11.interactable = false;
                    skill22.interactable = true;
                    skill33.interactable = false;
                    skill44.interactable = false;
                    skill55.interactable = false;
                    break;
                case 3:
                    skill11.interactable = false;
                    skill22.interactable = false;
                    skill33.interactable = true;
                    skill44.interactable = false;
                    skill55.interactable = false;
                    break;
                case 4:
                    skill11.interactable = false;
                    skill22.interactable = false;
                    skill33.interactable = false;
                    skill44.interactable = true;
                    skill55.interactable = false;
                    break;
                case 5:
                    skill11.interactable = false;
                    skill22.interactable = false;
                    skill33.interactable = false;
                    skill44.interactable = false;
                    skill55.interactable = true;
                    break;
                default:
                    skill11.interactable = false;
                    skill22.interactable = false;
                    skill33.interactable = false;
                    skill44.interactable = false;
                    skill55.interactable = false;
                    break;
            }
        }

        if (pt == 3)
        {
            switch (MainMenu.skillBuys)
            {
                case 1:
                    skill111.interactable = true;
                    skill222.interactable = false;
                    skill333.interactable = false;
                    skill444.interactable = false;
                    skill555.interactable = false;
                    break;
                case 2:
                    skill111.interactable = false;
                    skill222.interactable = true;
                    skill333.interactable = false;
                    skill444.interactable = false;
                    skill555.interactable = false;
                    break;
                case 3:
                    skill111.interactable = false;
                    skill222.interactable = false;
                    skill333.interactable = true;
                    skill444.interactable = false;
                    skill555.interactable = false;
                    break;
                case 4:
                    skill111.interactable = false;
                    skill222.interactable = false;
                    skill333.interactable = false;
                    skill444.interactable = true;
                    skill555.interactable = false;
                    break;
                case 5:
                    skill111.interactable = false;
                    skill222.interactable = false;
                    skill333.interactable = false;
                    skill444.interactable = false;
                    skill555.interactable = true;
                    break;
                default:
                    skill111.interactable = false;
                    skill222.interactable = false;
                    skill333.interactable = false;
                    skill444.interactable = false;
                    skill555.interactable = false;
                    break;
            }
        }

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





}
