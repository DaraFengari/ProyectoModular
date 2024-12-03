using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_followBehavior : StateMachineBehaviour
{
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float distanciaDetenerse;
    private float distanciaJugador;
    private Transform jugador;
    private Enemies enemy;

    [Header("Melee Attack Settings")]
    [SerializeField] private Transform controladorGolpeM;
    [SerializeField] private float radioGolpe = 10.5f;
    [SerializeField] private float meleeDelay = 1f; // Tiempo de espera entre golpes en melee
    private float meleeTimer = 0f;

    public float baseSpeed;
    public bool flee;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemy = animator.gameObject.GetComponent<Enemies>();
        controladorGolpeM = animator.gameObject.GetComponent<Transform>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        distanciaJugador = Vector2.Distance(animator.transform.position, jugador.position);
        if(distanciaJugador <= distanciaDetenerse)
        {
            meleeTimer += Time.deltaTime;
            Debug.Log("Entrando al ataque");

            if (meleeTimer >= meleeDelay)
            {
                meleeTimer = 0f;
                Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpeM.position, radioGolpe);
                foreach (Collider2D colisionador in objetos)
                {
                    if (colisionador.CompareTag("Player"))
                    {
                        // Evitar posibles errores si no tiene el componente Combate
                        if (colisionador.TryGetComponent(out Combate combatePlayer))
                        {
                            combatePlayer.TomarDaño(1);
                            Debug.Log("Golpe al jugador, activando flee");
                            flee = true;
                        }
                    }
                }
            }
            if (flee == false)
            {
                Debug.Log("Acercándose al jugador");
                animator.transform.position = Vector2.MoveTowards(animator.transform.position, jugador.position, velocidadMovimiento * Time.deltaTime);
            }
            else
            {
                Debug.Log("Huyendo del jugador");
                enemy.StartCoroutine(AccelerateForDuration());
                animator.transform.position = Vector2.MoveTowards(animator.transform.position, jugador.position, -velocidadMovimiento * Time.deltaTime);
            }
            enemy.Girar(jugador.position);
        }
        else
        {
            animator.SetTrigger("Detenerse");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
            
    }
    IEnumerator AccelerateForDuration()
    {
        flee = true;
        Debug.Log("Flee activado, huyendo");
        yield return new WaitForSeconds(2f);  // Huir durante 2 segundos
        flee = false;
        Debug.Log("Flee desactivado, acercándose");
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
