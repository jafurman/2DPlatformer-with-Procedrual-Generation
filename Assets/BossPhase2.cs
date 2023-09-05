using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhase2 : StateMachineBehaviour
{
    public float speed = 2.5f;
    Transform player;
    Rigidbody2D rb;
    public float attackRange;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 playerPosition = player.transform.position;
        Vector2 bossPosition = rb.transform.position;

        // Only consider the x-axis difference
        Vector2 direction = new Vector2(playerPosition.x - bossPosition.x, 0);

        // Normalize the direction vector to have a magnitude of 1
        direction.Normalize();

        // Apply the direction as a force to the rigidbody
        rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);

        if (direction.x < 0) // moving left
        {
            animator.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (direction.x > 0) // moving right
        {
            animator.transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Vector2.Distance(player.position, rb.position) <= attackRange)
        {
            animator.SetTrigger("Attack");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }
}
