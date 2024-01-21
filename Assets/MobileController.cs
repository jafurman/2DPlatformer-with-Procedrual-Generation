using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileController : MonoBehaviour
{
    public GameObject mobileControls;
    public static bool mobileControlsEnabled = false;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!mobileControlsEnabled)
        {
            mobileControls.SetActive(false);
        } else
        {
            mobileControls.SetActive(true);
        }
    }

    public void turnOnMobileControls()
    {
        mobileControlsEnabled = true;
        PlayerPrefs.SetInt("MobileGate", 1);

    }

    public void turnOffMobileControls()
    {
        mobileControlsEnabled = false;
        PlayerPrefs.SetInt("MobileGate", 1);
    }

    public void jump()
    {
        Debug.Log("CLICKING BUTTTOONNN");
        // CODE HERE USING PLAYER OBJECT TO DO CONTROLS
        // COPY PASTE CODE FROM THE PLAYERCONTROLLER
    }

    public void right()
    {

    }

    public void left()
    {

    }
}
