using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;

public class LightingController : MonoBehaviour
{
    public Light2D[] lights;

    public float darknessIntensity = 0.5f;

    public void Start()
    {

        lights = FindObjectsOfType<Light2D>();

        
        foreach (Light2D light in lights)
        {
            if (light.gameObject.CompareTag("Player"))
            {
                //do nothing
            } else
            {
              light.intensity = darknessIntensity;
            }

        }
    }
}
