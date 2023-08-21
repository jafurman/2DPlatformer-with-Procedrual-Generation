using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveDialogue : MonoBehaviour
{
    public GameObject totalCanvas;
    public Animator dialogueButton;

    private void Start()
    {
        totalCanvas.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            totalCanvas.SetActive(true);
            dialogueButton.SetBool("SlideIn", true);
        }

    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            totalCanvas.SetActive(false);
            dialogueButton.SetBool("SlideIn", false);
        }

    }



}
