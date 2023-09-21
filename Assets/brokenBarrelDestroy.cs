using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brokenBarrelDestroy : MonoBehaviour
{
    public static GameObject go;
    // Start is called before the first frame update
    void Start()
    {
        go = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void destroyBarrel()
    {
        Destroy(go);
    }
}
