using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catMoveTowardsPlayer : StateMachineBehaviour
{

    private float timeInRadius = 0f;
    private bool isIdling = false;
    private float speed = 1.4f;
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject cat = animator.gameObject;
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        float distance = Vector2.Distance(cat.transform.position, player.transform.position);

        if (distance > 3.5f)
        {
            timeInRadius = 0f;
            isIdling = false;

            Vector2 direction = (player.transform.position - cat.transform.position).normalized;
            direction.y = 0; 
            cat.transform.position += new Vector3(direction.x, 0, 0) * Time.deltaTime * speed; 

            if (player.transform.position.x < cat.transform.position.x)
            {
                cat.transform.localScale = new Vector3(Mathf.Abs(cat.transform.localScale.x), cat.transform.localScale.y, cat.transform.localScale.z);
            }
            else if (player.transform.position.x > cat.transform.position.x)
            {
                cat.transform.localScale = new Vector3(-Mathf.Abs(cat.transform.localScale.x), cat.transform.localScale.y, cat.transform.localScale.z);
            }
        }
        else
        {
            timeInRadius += Time.deltaTime;
            if (timeInRadius >= .05f && !isIdling)
            {
                isIdling = true;
                int idleAnimation = Random.Range(0, 2);
                if (idleAnimation == 0)
                {
                    animator.SetTrigger("sitCat");
                }
                else
                {
                    animator.SetTrigger("idleCat");
                }
            }
        }
    }
}
