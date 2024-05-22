using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catMoveTowardsPlayer : StateMachineBehaviour
{

    private float timeInRadius = 0f;
    private bool isIdling = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject cat = animator.gameObject;
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        float distance = Vector2.Distance(cat.transform.position, player.transform.position);

        if (distance > 1.0f)
        {
            // Reset the timer and idling flag when the cat is moving
            timeInRadius = 0f;
            isIdling = false;

            Vector2 direction = (player.transform.position - cat.transform.position).normalized;
            cat.transform.position += new Vector3(direction.x, direction.y, 0) * Time.deltaTime;

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


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
