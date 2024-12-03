using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tang_SeguirBehavior : StateMachineBehaviour
{
    [SerializeField] private float distanciaDetenerse;
    private float distanciaJugador;
    private Transform jugador;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator.gameObject.GetComponent<disparotang>().enabled = true;
        animator.gameObject.GetComponent<movetnk>().enabled = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        distanciaJugador = Vector2.Distance(animator.transform.position, jugador.position);

        if (distanciaJugador >= distanciaDetenerse)
        {
            animator.gameObject.GetComponent<disparotang>().enabled = false;
            animator.gameObject.GetComponent<movetnk>().enabled = false;
        }
        else
        {
            animator.gameObject.GetComponent<disparotang>().enabled = true;
            animator.gameObject.GetComponent<movetnk>().enabled = true;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<disparotang>().enabled = false;
        animator.gameObject.GetComponent<movetnk>().enabled = false;
        if (animator.gameObject.GetComponent<Animator>().GetBool("Runing"))
        {
            animator.gameObject.GetComponent<runtank>().enabled = true;
        }
            
    }

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
