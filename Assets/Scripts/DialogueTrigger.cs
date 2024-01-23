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
            popUpSound.PlayOneShot(popUpSound.clip);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
            nextDialogueSound.PlayOneShot(nextDialogueSound.clip);
        }

    }
}
