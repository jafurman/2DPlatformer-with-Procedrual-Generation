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
            if (!light.gameObject.CompareTag("Player") || (!light.gameObject.CompareTag("Spooder"))
                || (!light.gameObject.CompareTag("Souls"))
                || (!light.gameObject.CompareTag("Enemy"))
                || (!light.gameObject.CompareTag("Warden"))
                || (!light.gameObject.CompareTag("soul"))
                || (!light.gameObject.CompareTag("slideIndicator"))
                || (!light.gameObject.CompareTag("Danger")))
            {
                light.intensity = darknessIntensity;
            } else
            {
                light.intensity = darknessIntensity + 8;
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

    public static void crazyLights()
    {
        int WHICHONE = Random.Range(0, 2);
        Debug.Log(WHICHONE);
        if (WHICHONE == 1)
        {
            foreach (Light2D lig in lights)
            {
                float hue = Random.Range(0f, 1f);
                float value = Random.Range(0.7f, 1.0f);
                Color randomColor = Color.HSVToRGB(hue, 1, value);
                SpriteRenderer lightGo = lig.GetComponent<SpriteRenderer>();
                lightGo.color = randomColor;
                lig.color = randomColor;
                lig.intensity = 3;
            }
        } else if (WHICHONE == 0)
        {
            float hue = Random.Range(0f, 1f);
            float value = Random.Range(0.7f, 1.0f);
            Color randomColor = Color.HSVToRGB(hue, 1, value);
            foreach (Light2D lig in lights)
            {
                SpriteRenderer lightGo = lig.GetComponent<SpriteRenderer>();
                lightGo.color = randomColor;
                lig.color = randomColor;
                lig.intensity = 3;
            }
        }

    }


}
