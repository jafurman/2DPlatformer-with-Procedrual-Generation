using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class turnOffCollider : MonoBehaviour
{
    public TilemapCollider2D tm;

    public void Start()
    {
        tm.GetComponent<TilemapCollider2D>();
        tm.enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (RandomTilemap.started)
        {
            tm.enabled = false;
        }
    }
}
