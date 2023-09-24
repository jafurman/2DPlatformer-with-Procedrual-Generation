using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnMinusOneSoul : MonoBehaviour
{

    public GameObject minusOnePrefab;
    public static Vector3 spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
       spawnPoint = gameObject.transform.position;
    }

    public void spawnMinusOne()
    {
        StartCoroutine(destroyOne());
    }


   public IEnumerator destroyOne()
    {
        GameObject minusOne = Instantiate(minusOnePrefab, spawnPoint, Quaternion.identity);

        yield return new WaitForSeconds(1f);

        Destroy(minusOne);
    }
}
