using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatRespawn : MonoBehaviour
{
    public GameObject player;
    public GameObject cat;
    public float respawnDistance = 15f;
    public float respawnOffset = 1f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cat = GameObject.FindGameObjectWithTag("Cat");

    }
    void Update()
    {
        if (Vector3.Distance(cat.transform.position, player.transform.position) > respawnDistance)
        {
            StartCoroutine(RespawnCat());
        }
    }

    IEnumerator RespawnCat()
    {
        cat.SetActive(false);

        yield return new WaitForEndOfFrame();

        Vector3 respawnPosition = player.transform.position + player.transform.right * respawnOffset;
        cat.transform.position = respawnPosition;
        cat.SetActive(true);

    }
}
