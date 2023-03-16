using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float minY = 0f;
    [SerializeField] private float maxY = 5f;
    
    private bool movingUp = true;

    private void Update()
    {
        float currentY = transform.position.y;

        if (movingUp)
        {
            transform.position = new Vector2(transform.position.x, currentY + speed * Time.deltaTime);
        }
        else
        {
            transform.position = new Vector2(transform.position.x, currentY - speed * Time.deltaTime);
        }

        if (transform.position.y >= maxY)
        {
            movingUp = false;
        }
        else if (transform.position.y <= minY)
        {
            movingUp = true;
        }
    }
}

