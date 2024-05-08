using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossAnimationActivation : MonoBehaviour
{
    public dropperSpawner dpi;


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
}
