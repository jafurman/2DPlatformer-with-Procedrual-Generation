using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBoss : MonoBehaviour
{
    public GameObject eyeballPrefab;
    public Transform spawnPosition1;
    public Transform spawnPosition2;
    public Transform spawnPosition3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEye()
    {
        StartCoroutine(spawnEyes());

    }


    public IEnumerator spawnEyes()
    {
        yield return new WaitForSeconds(1.0f);
        Instantiate(eyeballPrefab, spawnPosition1.position, Quaternion.identity);
        Instantiate(eyeballPrefab, spawnPosition2.position, Quaternion.identity);
        yield return new WaitForSeconds(.5f);
        Instantiate(eyeballPrefab, spawnPosition3.position, Quaternion.identity);
    }
}
