using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnScreenSoulScoreDisplayManager : MonoBehaviour
{
    public int displayScore;

    public GameObject score10, score50, score100;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnAndDestroy());
    }

    public IEnumerator spawnAndDestroy()
    {
        GameObject obj;

        if (displayScore == 100)
        {
            obj = Instantiate(score100, transform.position, Quaternion.identity);
        }
        else if (displayScore == 10)
        {
            obj = Instantiate(score10, transform.position, Quaternion.identity);
        }
        else if (displayScore == 50)
        {
            obj = Instantiate(score50, transform.position, Quaternion.identity);
        }
        else
        {
            obj = null;
        }

        yield return new WaitForSeconds(.9f);

        if (obj != null)
        {
            Destroy(obj);
            Destroy(gameObject);
        }

    }


}
