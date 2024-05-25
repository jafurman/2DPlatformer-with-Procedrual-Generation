using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public GameObject boss1, boss2, nextLevelThing, tileMap;

    // Update is called once per frame
    void Update()
    {
        if (boss1 == null && boss2 == null)
        {
            Debug.Log("We getting called?");
            tileMap.SetActive(false);
            nextLevelThing.SetActive(true);
        }
    }
}
