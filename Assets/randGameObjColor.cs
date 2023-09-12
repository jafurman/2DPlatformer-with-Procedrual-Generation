using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randGameObjColor : MonoBehaviour
{

    Color minColor = new Color(0.5f, 0.5f, 0.5f); // Mid-gray
    Color maxColor = Color.white; // Maximum brightness (white)

    // Start is called before the first frame update
    void Start()
    {
        float randomR = Random.Range(minColor.r, maxColor.r);
        float randomG = Random.Range(minColor.g, maxColor.g);
        float randomB = Random.Range(minColor.b, maxColor.b);

        Color randomColor = new Color(randomR, randomG, randomB);

        Renderer renderer = GetComponent<Renderer>();
        renderer.material.color = randomColor;
    }
}
