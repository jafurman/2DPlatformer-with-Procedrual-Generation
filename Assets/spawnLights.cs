using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class spawnLights : MonoBehaviour
{
    public GameObject lightPos1;
    public GameObject lightPos2;
    public GameObject lightPos3;
    public GameObject lightPos4;

    public static Light2D light1;
    public static Light2D light2;
    public static Light2D light3;
    public static Light2D light4;

    // Start is called before the first frame update
    void Start()
    {
        light1 = lightPos1.GetComponent<Light2D>();
        light2 = lightPos2.GetComponent<Light2D>();
        light3 = lightPos3.GetComponent<Light2D>();
        light4 = lightPos4.GetComponent<Light2D>();
    }

    public static IEnumerator startLights()
    {
        Debug.Log("calling the function");
        light1.intensity = 7f;
        yield return new WaitForSeconds(.8f);
        light2.intensity = 7f;
        yield return new WaitForSeconds(.8f);
        light3.intensity = 7f;
        yield return new WaitForSeconds(.8f);
        light4.intensity = 7f;
    }
}
