using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnSoul : MonoBehaviour
{

    public GameObject spawningPrefab;
    public GameObject soulPrefab;

    public AudioSource se;
    public AudioClip clip;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnStart());

                    if ((clip) != null && se != null)
                {
                    //once hit this sound will play
                    se.PlayOneShot(clip);
                }
    }

    public IEnumerator spawnStart()
    {
        Vector3 spawnPos = gameObject.transform.position;
        GameObject spawnedObject = Instantiate(spawningPrefab, spawnPos, Quaternion.identity);
        yield return new WaitForSeconds(.75f); //length of the animaiton
        Destroy(spawnedObject);
        GameObject newSoul = Instantiate(soulPrefab, spawnPos, Quaternion.identity);
        Destroy(gameObject);
    }
}

