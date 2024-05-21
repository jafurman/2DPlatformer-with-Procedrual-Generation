using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinusoidalMove : MonoBehaviour
{

	[SerializeField]
	float moveSpeed = 5f;

	[SerializeField]
	float frequency = 20f;


    float magnitude;

	bool facingRight = true;

	[SerializeField]
	float leftBound = 0f;

	[SerializeField]
	float rightBound = 0f;

	Vector3 pos, localScale;




    // Start is called before the first frame update
    void Start()
    {
        CheckWhereToFace();
        pos = transform.position;

        localScale = transform.localScale;


        magnitude = Random.Range(0.2f, 0.4f);

    }

    void Update()
    {
        if (facingRight)
        {
            MoveRight();
            transform.rotation = Quaternion.LookRotation(Vector3.right);
        }
        else
        {
            Debug.Log("turning left");
            MoveLeft();
            transform.rotation = Quaternion.LookRotation(Vector3.left);
        }
    }

    void CheckWhereToFace()
    {

    	if(pos.x < leftBound)
    	{
    		facingRight = true; 
    	} else if (pos.x > rightBound)
    	{
    		facingRight = false; 
    	}

    	if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
    	{
    		
    		localScale.x *= -1;
    		

    	}

    	transform.localScale = localScale;
    	
    }

    void MoveRight()
    {
    	pos += transform.right * Time.deltaTime * moveSpeed;
    	transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
    }

    void MoveLeft()
    {
    	pos -= transform.right * Time.deltaTime * moveSpeed;
    	transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
    	
    }





}
