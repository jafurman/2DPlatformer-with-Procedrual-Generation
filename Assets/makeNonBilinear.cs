using UnityEngine;

public class makeNonBilinear : MonoBehaviour
{
    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            Texture texture = sr.sprite.texture;
            texture.filterMode = FilterMode.Point;
        }
    }
}