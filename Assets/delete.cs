using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class delete : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(destroyGO());
    }


    public IEnumerator destroyGO()
    {
        yield return new WaitForSeconds(.2f);

        Destroy(gameObject);
    }


}
