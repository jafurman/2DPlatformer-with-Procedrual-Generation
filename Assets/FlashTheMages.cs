using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashTheMages : MonoBehaviour
{
    public GameObject[] mages;

    public IEnumerator FlashDEMMAGES()
    {

        foreach (GameObject mage in mages)
        {
            Image mageSprite = mage.GetComponent<Image>();


            if (mageSprite != null)
            {
                RectTransform rectTransform = mageSprite.rectTransform;
                rectTransform.localScale *= 1.5f;
                Color currentColor = mageSprite.color;
                Color newColor = Color.white;
                mageSprite.color = newColor;
                yield return new WaitForSeconds(.1f);
                mageSprite.color = currentColor;
                rectTransform.localScale /= 1.5f;
            }
            
            

        }

        StartCoroutine(FlashDEMMAGES());

     

    }

   
}
