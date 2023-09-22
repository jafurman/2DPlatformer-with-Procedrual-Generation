using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cMage : MonoBehaviour
{
    public GameObject shootPrefab;
    public Transform spawnPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void shoot()
    {
        Instantiate(shootPrefab, spawnPos.position, Quaternion.identity);
    }
}
