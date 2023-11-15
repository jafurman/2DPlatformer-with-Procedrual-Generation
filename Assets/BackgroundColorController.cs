using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColorController : MonoBehaviour
{

    public GameObject[] backgrounds;

    public bool randomBgColor;
    // Start is called before the first frame update
    void Start()
    {
        backgrounds = GameObject.FindGameObjectsWithTag("BACKGROUND");

        if (randomBgColor && RandomTilemap.tmColor != null)
        {
            changeBgColor();
        }
    }


    public void changeBgColor()
    {
        Debug.Log("Changing color of BG");
        Color matchColor = RandomTilemap.tmColor;
        foreach(GameObject go in backgrounds)
        {
            SpriteRenderer goSprite = go.GetComponent<SpriteRenderer>();
            goSprite.color = matchColor;
        }
    }


}
