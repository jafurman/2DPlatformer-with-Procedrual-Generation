using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{

	public int soulValue;

	public AudioSource playSound;
    public AudioClip sound;

    public SoulScoreManager ssm;

    private void Start()
    {
        ssm = GameObject.FindGameObjectWithTag("soulScoreManager").GetComponent<SoulScoreManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    	if (collision.tag == "Player")
        {
            ssm.addPoints(10);
            playSound.PlayOneShot(sound);
            ScoreManager.instance.ChangeScore(soulValue);
            Destroy(gameObject);
    	}
    }
}
