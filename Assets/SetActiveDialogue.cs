using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveDialogue : MonoBehaviour
{
    public GameObject totalCanvas;

    private void Start()
    {
        totalCanvas.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        totalCanvas.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        totalCanvas.SetActive(false);
    }



}
