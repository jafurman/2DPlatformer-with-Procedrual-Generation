using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class impactAudio : MonoBehaviour
{
    public AudioSource audsrc;
    public AudioClip daClip;


    // Start is called before the first frame update
    void Start()
    {
        if (audsrc != null && daClip != null) { audsrc.PlayOneShot(daClip); }
    }
}
