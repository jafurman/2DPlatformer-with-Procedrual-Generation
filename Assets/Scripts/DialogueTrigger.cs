using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public AudioSource popUpSound;
    public AudioSource nextDialogueSound;

    public bool isOpeningScene;

    public void Start()
    {
        if (isOpeningScene)
        {
            TriggerDialogue();
        }
    }
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    public void Update()
    {
       if (Input.GetKeyDown(KeyCode.I))
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            if (popUpSound != null)
            {
                popUpSound.PlayOneShot(popUpSound.clip);
            }

        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
            if (nextDialogueSound != null)
            {
                nextDialogueSound.PlayOneShot(nextDialogueSound.clip);
            }

        }

    }
}
