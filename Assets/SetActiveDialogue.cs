using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveDialogue : MonoBehaviour
{
    public GameObject totalCanvas;
    public Animator dialogueButton;

    public FlashTheMages FlashTheMagesInstance;

    private void Start()
    {
        totalCanvas.SetActive(false);

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            StartCoroutine(FlashTheMagesInstance.FlashDEMMAGES());
            totalCanvas.SetActive(true);
            dialogueButton.SetBool("SlideIn", true);
        }

    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            StartCoroutine(waitASec());
        }

    }


    public IEnumerator waitASec()
    {
        dialogueButton.SetBool("SlideIn", false);
        yield return new WaitForSeconds(.5f);
        totalCanvas.SetActive(false);

    }



}
