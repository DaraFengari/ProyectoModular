using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunsliner_SeguirBehavior : StateMachineBehaviour
{
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float distanciaDetenerse;
    [SerializeField] private Transform controladorDisparo;
    [SerializeField] private GameObject bala;
    private float distanciaJugador;
    private Transform jugador;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        controladorDisparo = animator.gameObject.GetComponent<Transform>();
        animator.gameObject.GetComponent<disparo>().enabled = true;
        animator.gameObject.GetComponent<Gunslinger>().enabled = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        distanciaJugador = Vector2.Distance(animator.transform.position, jugador.position);

        if (distanciaJugador >= distanciaDetenerse)
        {
            animator.gameObject.GetComponent<disparo>().enabled = false;
            animator.gameObject.GetComponent<Gunslinger>().enabled = false;
        }
        else
        {
            animator.gameObject.GetComponent<disparo>().enabled = true;
            animator.gameObject.GetComponent<Gunslinger>().enabled = true;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<disparo>().enabled = false;
        animator.gameObject.GetComponent<Gunslinger>().enabled = false;
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
