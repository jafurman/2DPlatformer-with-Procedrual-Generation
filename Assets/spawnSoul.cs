using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnSoul : MonoBehaviour
{

    public GameObject spawningPrefab;
    public GameObject soulPrefab;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnStart());
    }

    public IEnumerator spawnStart()
    {
        Vector3 spawnPos = gameObject.transform.position;
        GameObject spawnedObject = Instantiate(spawningPrefab, spawnPos, Quaternion.identity);
        yield return new WaitForSeconds(.75f); //length of the animaiton
        Destroy(spawnedObject);
        GameObject newSoul = Instantiate(soulPrefab, spawnPos, Quaternion.identity);
    }
}
