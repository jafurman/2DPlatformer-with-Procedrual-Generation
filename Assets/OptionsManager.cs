using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    public GameObject OptionsMenu;
    public GameObject MainOptionsPanel;
    public GameObject SaveGamePanel;
    public GameObject SkillPanel;
    public GameObject ControlsPanel;
    // Start is called before the first frame update
    void Start()
    {
        OptionsMenu.SetActive(false);
        //MainOptionsPanel.SetActive(false);
        SaveGamePanel.SetActive(false);
        SkillPanel.SetActive(false);
        ControlsPanel.SetActive(false);

    }

    public void openMainMenu(){
        Debug.Log("Trying to open panel"); OptionsMenu.SetActive(true);}
    public void closeMainMenu(){OptionsMenu.SetActive(false);}

    public void openMainOptionsPanel(){MainOptionsPanel.SetActive(true);}
    public void closeMainOptionsPanel() { MainOptionsPanel.SetActive(false);}

    public void openSaveGamePanel() { SaveGamePanel.SetActive(true);}
    public void closeSaveGamePanel() { SaveGamePanel.SetActive(false);}

    public void openSkillPanel() { SkillPanel.SetActive(true); }
    public void closeSkillPanel() { SkillPanel.SetActive(false);}

    public void openControlsPanel() { ControlsPanel.SetActive(true);}
    public void closeControlsPanel() { ControlsPanel.SetActive(false);}
}
