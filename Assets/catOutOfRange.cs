using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catOutOfRange : StateMachineBehaviour
{
    GameObject player;
    float distanceToPlayer;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        distanceToPlayer = Vector2.Distance(player.transform.position, animator.transform.position);

        if (distanceToPlayer > 1.5f)
        {
            animator.SetTrigger("walkingCat");
            Debug.Log("Switching");
        }
    }
}
