using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noiseAnimationEnter : StateMachineBehaviour
{

    public float moveDistance = .001f; // Distance to move

    private bool right;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 currentPosition = animator.transform.position;

        //if the player isn't grounded I want there to be more knockback
        if ( !PlayerController.grounded)
        {
            moveDistance = moveDistance * 2;
        } 


        if (PlayerController.FacingRight)
        {
            right = true;
        } else
        {
            right = false;
        }

        
        
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 currentPosition = animator.transform.position;

        //actually does the knockback for right
        if (right)
        {
            Vector3 newPosition = new Vector3(currentPosition.x + moveDistance, currentPosition.y, currentPosition.z);
            animator.transform.position = newPosition;
        }

        //knockback for player facing left
        if (!right)
        {
            Vector3 newPosition = new Vector3(currentPosition.x - moveDistance, currentPosition.y, currentPosition.z);
            animator.transform.position = newPosition;
        }
    }
}
