using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulScoreManager : MonoBehaviour
{
    public int soulSlots;
    public Text addedPoints;
    public Text change;
    public Text levelDisplay;
    public static float speedMultiplier;
    public static int latest;
    public static SoulScoreManager instance;

    public Button skill1;
    public Button skill2;
    public Button skill3;
    public Button skill4;
    public Button skill5;

    public Button skill11, skill22, skill33, skill44, skill55, skill111, skill222, skill333, skill444, skill555;

    public GameObject player;
    public GameObject pointsAbovePlayer;
    public GameObject XPBar;
    public int level;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        MainMenu.skillBuys = PlayerPrefs.GetInt("SkillBuys");

        if (PlayerPrefs.GetInt("SkillBuys") >= 5)
        {
            skill1.interactable = false;
            skill2.interactable = false;
            skill3.interactable = false;
            skill4.interactable = false;
            skill5.interactable = false;
        }

        PlayerPrefs.SetInt("currentXP", PlayerPrefs.GetInt("soulScore"));

        level = Mathf.FloorToInt((float)PlayerPrefs.GetInt("currentXP") / 1000f);
        PlayerPrefs.SetInt("PlayerLevel", level);

        if (PlayerPrefs.GetInt("currentXP") > 1000)
        {
            PlayerPrefs.SetInt("currentXP", PlayerPrefs.GetInt("currentXP") % 1000);
        }
        PlayerPrefs.Save();
        UpdateXPBar();

        latest = 0;
        speedMultiplier = 1;



    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetInt("currentXP", PlayerPrefs.GetInt("soulScore"));
        level = Mathf.FloorToInt((float)PlayerPrefs.GetInt("currentXP") / 1000f);

        int currentScore = PlayerPrefs.GetInt("soulScore");
        addedPoints.text = "Soul Score: " + "[" +currentScore + "]";
        change.text = "Latest change: " + " [" + latest + "]";


        level = Mathf.FloorToInt((float)PlayerPrefs.GetInt("currentXP") / 1000f);
        PlayerPrefs.SetInt("PlayerLevel", level);
        int currentLevel = PlayerPrefs.GetInt("PlayerLevel");

        if (levelDisplay != null)
        {
            levelDisplay.text = "Level: [" + currentLevel + "] ";
        }

        PlayerPrefs.SetInt("currentXP", PlayerPrefs.GetInt("soulScore"));
        if (PlayerPrefs.GetInt("currentXP") > 1000)
        {
            PlayerPrefs.SetInt("currentXP", PlayerPrefs.GetInt("currentXP") % 1000);
        }

        PlayerPrefs.Save();
        UpdateXPBar();
    }

    public void UpdateXPBar()
    {
        float currentXP = PlayerPrefs.GetInt("currentXP");
        float scaleX = currentXP / 1000;
        if (XPBar != null)
        {
            XPBar.GetComponent<RectTransform>().pivot = new Vector2(0, 0.5f);
            XPBar.transform.localScale = new Vector3(scaleX, XPBar.transform.localScale.y, XPBar.transform.localScale.z);
        }
    }


    public void addPoints(int points)
    {
        StartCoroutine(addedPointsAbovePlayer(points));

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
        latest = -points;

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



    public IEnumerator addedPointsAbovePlayer(int points)
    {
        Vector3 playerSpawnPos = player.transform.position;
        playerSpawnPos.y += .15f;
        GameObject pointsAdded = pointsAbovePlayer;
        OnScreenSoulScoreDisplayManager scriptInstance = pointsAdded.GetComponent<OnScreenSoulScoreDisplayManager>();
        scriptInstance.displayScore = points;
        Instantiate(pointsAdded, playerSpawnPos, Quaternion.identity);
        yield return new WaitForSeconds(.9f);
        Destroy(pointsAdded);
    }

}
