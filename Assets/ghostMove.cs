using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostMove : StateMachineBehaviour
{
    public float speed = 2.5f;
    Transform player;
    Rigidbody2D rb;
    private SpriteRenderer sprite;
    public float attackRange;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        sprite = animator.GetComponent<SpriteRenderer>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 direction = player.transform.position - rb.transform.position;
        rb.MovePosition(rb.position + direction.normalized * speed * Time.fixedDeltaTime);

        if (direction.x < 0) // moving left
        {
            sprite.flipX = true; // flip the sprite
        }
        else if (direction.x > 0) // moving right
        {
            sprite.flipX = false; // do not flip the sprite
        }
        // no need to flip if the velocity is zero

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
