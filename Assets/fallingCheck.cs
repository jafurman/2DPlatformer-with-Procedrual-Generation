using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingCheck : StateMachineBehaviour
{
    public GameObject bossCamera;

    public BossHPBar hpBar;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hpBar = animator.GetComponent<BossHPBar>();

        if (hpBar != null)
        {
            hpBar.HC1.SetActive(false);
            hpBar.HC2.SetActive(false);
            hpBar.HC3.SetActive(false);
            hpBar.HC4.SetActive(false);
            hpBar.HC5.SetActive(false);
            hpBar.HC6.SetActive(false);
            hpBar.HC7.SetActive(false);
        }

        bossFightCamera bossCam = bossCamera.GetComponent<bossFightCamera>();
        bossCam.BackToPlayerCamera();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
