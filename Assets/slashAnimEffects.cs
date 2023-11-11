using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class slashAnimEffects : MonoBehaviour
{
    public Light2D light;
    public static bool isWanted = false;
    // Start is called before the first frame update
    void Start()
    {
        if (isWanted)
        {
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();

            Color randomColor = GetRandomBrightColor();
            sprite.color = randomColor;
            light.color = randomColor;
        }

    }

    private Color GetRandomBrightColor()
    {
        float randomHue = Random.Range(0f, 1f);
        float saturation = 1f;
        float brightness = .5f;
        Color randomColor = Color.HSVToRGB(randomHue, saturation, brightness);

        return randomColor;
    }
}
