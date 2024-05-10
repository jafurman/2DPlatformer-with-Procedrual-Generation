using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossAnimationActivation : MonoBehaviour
{
    public dropperSpawner dpi;
    public GameObject eyeballPrefab;
    public Transform p1, p2, p3;


    public void everyOtherTop()
    {
        dpi.DropEveryOther();
    }

    public void everyThirdTop()
    {
        dpi.DropEveryThird();
    }

    public void leftToRightTop()
    {
        dpi.LeftWipeDrop();
    }

    public void rightToLeftDrop()
    {
        dpi.RightWipeDrop();
    }

    public void leftHalfDrop()
    {
        dpi.DropLeftHalf();
    }

    public void rightHalfDrop()
    {
        dpi.DropRightHalf();
    }

    public void randomDrop()
    {
        dpi.RandomPositionDrop();
    }

    public void wipeLeft()
    {
        StartCoroutine(dpi.LeftWipeDrop());
    }

    public void RightLeft()
    {
        StartCoroutine(dpi.RightWipeDrop());
    }

    public void spawnEyesP1()
    {
        GameObject eyeball = Instantiate(eyeballPrefab, p1.position, Quaternion.identity);
    }

    public void spawnEyesP2()
    {
        GameObject eyeball = Instantiate(eyeballPrefab, p2.position, Quaternion.identity);
    }

    public void spawnEyesP3()
    {
        GameObject eyeball = Instantiate(eyeballPrefab, p3.position, Quaternion.identity);
    }
}
