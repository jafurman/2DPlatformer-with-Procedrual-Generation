using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillChoiceManager : MonoBehaviour
{
    public static int choice;
    public GameObject b1; //agility
    public GameObject b2; //soul
    public GameObject b3; //bulky

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            agilityButton();
        }
        if (Input.GetKey(KeyCode.G))
        {
            soulButton();
        }
        if (Input.GetKey(KeyCode.H))
        {
            bulkyButton();
        }
        if (Input.GetKey(KeyCode.B))
        {
            resetChoice();
        }


    }

    public void soulButton()
    {
        choice = 1;
        PlayerPrefs.SetInt("PlayerType", choice);
        PlayerPrefs.Save();
        b1.SetActive(false);
        b2.SetActive(true);
        b3.SetActive(false);
    }

    public void agilityButton()
    {
        choice = 2;
        PlayerPrefs.SetInt("PlayerType", choice);
        PlayerPrefs.Save();
        b1.SetActive(true);
        b2.SetActive(false);
        b3.SetActive(false);
    }

    public void bulkyButton()
    {
        choice = 3;
        PlayerPrefs.SetInt("PlayerType", choice);
        PlayerPrefs.Save();
        b1.SetActive(false);
        b2.SetActive(false);
        b3.SetActive(true);
    }

    public void resetChoice()
    {
        choice = 0;
        PlayerPrefs.SetInt("PlayerType", choice);
        PlayerPrefs.Save();
        b1.SetActive(true);
        b2.SetActive(true);
        b3.SetActive(true);
    }

}
