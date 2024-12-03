using System.Collections;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    // Enums para los estados de combate
    private enum CombatState { MeleeAttack, RangedAttack }
    private CombatState currentState = CombatState.RangedAttack;
    [SerializeField] private GameObject warper,explosion;
    [Header("Melee Attack Settings")]
    [SerializeField] private Transform controladorGolpeM;
    [SerializeField] private float radioGolpe = 10.5f;
    [SerializeField] private float meleeDelay = 1f; // Tiempo de espera entre golpes en melee
    private float meleeTimer = 0f;

    [Header("Ranged Attack Settings")]
    [SerializeField] private Transform controladorDisparo;
    [SerializeField] private GameObject bala;
    [SerializeField] private float rangedDelay; // Tiempo entre disparos
    private float rangedTimer = 0f;

    private float switchTimer = 0f; // Timer para alternar entre ataques
    private bool fleeing = false;

    private void Update()
    {
        // Gestiona el temporizador de cambio de estado
        switchTimer += Time.deltaTime;

        if (switchTimer >= 3f)
        {
            switchTimer = 0f;
            ToggleCombatState();
        }

        // Ejecución del estado actual
        switch (currentState)
        {
            case CombatState.MeleeAttack:
                MeleeAttack();
                break;

            case CombatState.RangedAttack:
                RangedAttack();
                break;
        }
    }

    // Alterna entre ataques cuerpo a cuerpo y a distancia
    private void ToggleCombatState()
    {
        fleeing = !fleeing;
        currentState = fleeing ? CombatState.RangedAttack : CombatState.MeleeAttack;
    }

    // Ataque cuerpo a cuerpo
    private void MeleeAttack()
    {
        meleeTimer += Time.deltaTime;

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
                        combatePlayer.TomarDaño(3);
                        GetComponent<Seeker>().flee = true; // Enemigo huye después del golpe
                    }
                }
            }
        }
    }

    // Ataque a distancia
    private void RangedAttack()
    {
        rangedTimer += Time.deltaTime;

        if (rangedTimer >= rangedDelay)
        {
            rangedTimer = 0f;
            Disparar();
        }
    }

    // Disparar proyectil
    private void Disparar()
    {
        Instantiate(bala, controladorDisparo.position, controladorDisparo.rotation);
        Instantiate(bala, controladorDisparo.position, controladorDisparo.rotation);
        Instantiate(bala, controladorDisparo.position, controladorDisparo.rotation);
        Instantiate(bala, controladorDisparo.position, controladorDisparo.rotation);
        Instantiate(bala, controladorDisparo.position, controladorDisparo.rotation);
    }

    // Visualización en editor de la zona de golpe cuerpo a cuerpo
    private void OnDrawGizmosSelected()
    {
        if (controladorGolpeM != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(controladorGolpeM.position, radioGolpe);
        }
    }

    private void OnDestroy()
    {
        Instantiate(explosion, controladorDisparo.position, controladorDisparo.rotation);
        Instantiate(warper, controladorDisparo.position, controladorDisparo.rotation);
    }
}
