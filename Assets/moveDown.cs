using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveDown : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float position = transform.position.y;
        transform.position = new Vector2(transform.position.x, position - speed * Time.deltaTime);
    }
}
