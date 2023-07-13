using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{

	public int soulValue;

	public AudioSource playSound;
    public AudioClip sound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
    	if (collision.tag == "Player")
        {
            playSound.PlayOneShot(sound);
            ScoreManager.instance.ChangeScore(soulValue);
            Destroy(gameObject);
    	}
    }
}
