using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class finalboss : MonoBehaviour
{
    // Enums para los estados de combate
    private enum CombatState { flame, BombAttack }
    private CombatState currentState = CombatState.flame;
    [SerializeField] private GameObject warper, explosion;
    int var = 0;
    [SerializeField] private float radioGolpe = 10.5f;
    [SerializeField]
    public Transform controladorfuego;
    private float firedelay = 5f;
    [Header("Melee Attack Settings")]
    [SerializeField] private Transform controladorDisparo;
    [SerializeField] private float meleeDelay = 1f; // Tiempo de espera entre golpes en melee
    private float meleeTimer = 0f;
    private float rangedTimer = 0f;

    [Header("Tank Attack Settings")]
    [SerializeField] private GameObject balatnk;
    private float timer;
    private bool switcher, first = true;

    [Header("Flame Attack Settings")]
    [SerializeField] private GameObject Fire;
    // Start is called before the first frame update

    private float switchTimer = 0f; // Timer para alternar entre ataques
    private bool fleeing = false;

    private void Update()
    {
        // Gestiona el temporizador de cambio de estado
        switchTimer += Time.deltaTime;
        Debug.Log(switchTimer);
        if (switchTimer >= 5f)
        {
            switchTimer = 0f;
            gameObject.GetComponent<rinomove>().runaway = false;
            ToggleCombatState();
        }
            switch (currentState)
            {
                case CombatState.flame:
                    FlameAttack();
                    gameObject.GetComponent<rinomove>().enabled = true;
                    break;

            case CombatState.BombAttack:
                    gameObject.GetComponent<rinomove>().enabled = false;
                    MeleeAttack();
                    break;
            }
            Debug.Log(var);

    }
    
    

    private void ToggleCombatState()
    {
        fleeing = !fleeing;
        currentState = fleeing ? CombatState.BombAttack : CombatState.flame;
    }


    // Ataque cuerpo a cuerpo
    private void MeleeAttack()
    {
        meleeTimer += Time.deltaTime;

        if (meleeTimer >= meleeDelay)
        {
            meleeTimer = 0f;
            Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorDisparo.position, radioGolpe);
            foreach (Collider2D colisionador in objetos)
            {
                if (colisionador.CompareTag("Player"))
                {
                    // Evitar posibles errores si no tiene el componente Combate
                    if (colisionador.TryGetComponent(out Combate combatePlayer))
                    {
                        combatePlayer.TomarDaño(1);
                        Instantiate(explosion, controladorDisparo.position, controladorDisparo.rotation);
                        GetComponent<rinomove>().runaway = true; // Enemigo huye después del golpe
                    }
                }
            }
        }
    }

    private void FlameAttack()
    {
        rangedTimer += Time.deltaTime;
        if (first)
        {
            Instantiate(Fire, controladorDisparo.position, controladorDisparo.rotation);
            first = false;
        }
        else if (rangedTimer >= firedelay)
        {
            rangedTimer = 0f;
            Instantiate(Fire, controladorDisparo.position, controladorDisparo.rotation);
        }

    }

    private void OnDestroy()
    {
        Instantiate(explosion, controladorDisparo.position, controladorDisparo.rotation);
        Instantiate(warper, controladorDisparo.position, controladorDisparo.rotation);
    }
}
