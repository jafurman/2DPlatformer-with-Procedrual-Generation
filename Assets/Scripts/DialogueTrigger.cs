using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public bool isOpeningScene;

    public void Start()
    {
        if (isOpeningScene)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
    }
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    public void Update()
    {
       if (Input.GetKey(KeyCode.I))
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
        }

    }
}
