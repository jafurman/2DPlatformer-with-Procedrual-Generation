using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDcanShootSoulSpawner : MonoBehaviour
{
    public GameObject cantShootPrefab;
    public GameObject player;
    public Vector3 spawnPos;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        spawnPos = player.transform.position;

        if (PlayerController.FacingRight)
        {
            spawnPos.x += .4f;
        } else if (!PlayerController.FacingRight)
        {
            spawnPos.x -= .4f;
        }


        if (Weapon.canShoot == false && Input.GetKeyDown(KeyCode.Space) || (Input.GetKeyDown(KeyCode.Space) && Weapon.shotsLeft <= 0))
        {
            StartCoroutine(SpawnAndDestroy());
        }
    }

    public IEnumerator SpawnAndDestroy()
    {
        GameObject cantShootSpawned = Instantiate(cantShootPrefab, spawnPos, Quaternion.identity);

        yield return new WaitForSeconds(1.3f);

        Destroy(cantShootSpawned);
    }
}
