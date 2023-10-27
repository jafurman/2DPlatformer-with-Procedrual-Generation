using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyIn : MonoBehaviour
{
    public float secondsBeforeDelete;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyInn(secondsBeforeDelete));
    }

    private IEnumerator DestroyInn(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

}
