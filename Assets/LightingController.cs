using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;

public class LightingController : MonoBehaviour
{
    public static Light2D[] lights;

    public float darknessIntensity = 0.5f;

    public void Start()
    {

        lights = FindObjectsOfType<Light2D>();

        
        foreach (Light2D light in lights)
        {
            if (!light.gameObject.CompareTag("Player"))
            {
                light.intensity = darknessIntensity;
            }
        }
    }

    public static void turnBright()
    {
        foreach (Light2D light in lights)
        {
            light.intensity = 5f;
        }
    }
}
