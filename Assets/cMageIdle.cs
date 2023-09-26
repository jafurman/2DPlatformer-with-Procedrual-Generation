using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cMageIdle : StateMachineBehaviour
{
    private float timer = 0f;
    public float shootInterval = 5f; 


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0f;
        animator.ResetTrigger("takeDamage");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;

        if (timer >= shootInterval)
        {
            timer = 0f;

 
            animator.SetTrigger("Shoot");
        }
    }
}