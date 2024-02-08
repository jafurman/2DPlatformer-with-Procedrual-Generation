using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPanelManager : MonoBehaviour
{
    public GameObject agilityB;
    public GameObject soulB;
    public GameObject bulkyB;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int choice = PlayerPrefs.GetInt("PlayerType");
        if (choice == 1)
        {
            agilityB.SetActive(false);
            soulB.SetActive(true);
            bulkyB.SetActive(false);
        }
        else if (choice == 2)
        {
            agilityB.SetActive(true);
            soulB.SetActive(false);
            bulkyB.SetActive(false);
        }
        else if (choice == 3)
        {
            agilityB.SetActive(false);
            soulB.SetActive(false);
            bulkyB.SetActive(true);
        }
        else
        {
            //Bulky is default
            agilityB.SetActive(false);
            soulB.SetActive(false);
            bulkyB.SetActive(true);
        }
    }
}
