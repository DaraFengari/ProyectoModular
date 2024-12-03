using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombseguir : StateMachineBehaviour
{
    [SerializeField] private float distanciaDetenerse;
    private float distanciaJugador;
    private Transform jugador;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator.gameObject.GetComponent<movebomber>().enabled = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        distanciaJugador = Vector2.Distance(animator.transform.position, jugador.position);
        if (distanciaJugador >= distanciaDetenerse)
        {
            animator.gameObject.GetComponent<movebomber>().enabled = false;
        }
        else
        {
            animator.gameObject.GetComponent<movebomber>().enabled = true;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<movebomber>().enabled = false;
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
