using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class findDialogueAndStart : MonoBehaviour
{

    public DialogueManager dm;
    // Start is called before the first frame update
    void Start()
    {
        dm = FindObjectOfType<DialogueManager>();
    }
}
