using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthBarColor : MonoBehaviour
{
    public float minScale = 0f;
    public float maxScale = .6f;

    void Update()
    {
        float currentScaleX = transform.localScale.x;
        currentScaleX = Mathf.Clamp(currentScaleX, minScale, maxScale);
        float normalizedValue = (currentScaleX - minScale) / (maxScale - minScale);
        Color lerpedColor = Color.Lerp(Color.red, Color.green, normalizedValue);
        GetComponent<SpriteRenderer>().material.color = lerpedColor;
    }
}
