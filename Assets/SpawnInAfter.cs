using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInAfter : MonoBehaviour
{
    public GameObject player, Boss1, Boss2;
    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<SpriteRenderer>().enabled = false;
        Boss1.SetActive(false);
        Boss2.SetActive(false);

        StartCoroutine(SpawnAfta());
    }

    IEnumerator SpawnAfta()
    {
        yield return new WaitForSeconds(4.61f);

        player.GetComponent<SpriteRenderer>().enabled = true;
        Boss1.SetActive(true);
        Boss2.SetActive(true);
    }


}
