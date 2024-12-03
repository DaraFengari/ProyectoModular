using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyBoss : MonoBehaviour
{
    private enum CombatState { FlameAttack, RangedAttack }
    private CombatState currentState = CombatState.RangedAttack;

    [Header("Flame Attack Settings")]
    [SerializeField] private GameObject Fire;
    private float regreso;
    [SerializeField] private GameObject warper, explosion;
    [Header("Ranged Attack Settings")]
    [SerializeField] private Transform controladorDisparo;
    [SerializeField] private GameObject bala;
    [SerializeField] private float rangedDelay = 2f, firedelay = 5f; // Tiempo entre disparos
    private float rangedTimer = 0f;
    private bool switcher,first=true;
    // Start is called before the first frame update

    private float switchTimer = 0f; // Timer para alternar entre ataques

    // Update is called once per frame
    void Update()
    {
        // Gestiona el temporizador de cambio de estado
        switchTimer += Time.deltaTime;

        if (switchTimer >= 4f)
        {
            gameObject.GetComponent<Movebunny>().enabled = true;
            switchTimer = 0f;
            ToggleCombatState();
        }

        // Ejecución del estado actual
        switch (currentState)
        {
            case CombatState.FlameAttack:
                FlameAttack();
                gameObject.GetComponent<Movebunny>().enabled = false;
                break;

            case CombatState.RangedAttack:
                RangedAttack();
                break;
        }
    }

    private void ToggleCombatState()
    {
        switcher = !switcher;
        currentState = switcher ? CombatState.RangedAttack : CombatState.FlameAttack;
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

    private void Disparar()
    {
        Instantiate(bala, controladorDisparo.position, controladorDisparo.rotation);
    }

    private void FlameAttack()
    {
        rangedTimer += Time.deltaTime;
        if (first)
        {
            if(GameObject.FindWithTag("Player").GetComponent<Transform>().position.x > gameObject.transform.position.x)
            Instantiate(Fire, controladorDisparo.position, controladorDisparo.rotation = Quaternion.Euler(0, 0, 0));
            else
            {
                Instantiate(Fire, controladorDisparo.position, controladorDisparo.rotation = Quaternion.Euler(0, 180, 0));
            }
            first = false;
        }else if (rangedTimer >= firedelay)
        {
            rangedTimer = 0f;
            if (GameObject.FindWithTag("Player").GetComponent<Transform>().position.x > gameObject.transform.position.x)
                Instantiate(Fire, controladorDisparo.position, controladorDisparo.rotation = Quaternion.Euler(0, 0, 0));
            else
            {
                Instantiate(Fire, controladorDisparo.position, controladorDisparo.rotation = Quaternion.Euler(0, 180, 0));
            }
        }
            
    }
    private void OnDestroy()
    {
        Instantiate(explosion, controladorDisparo.position, controladorDisparo.rotation);
        Instantiate(warper, controladorDisparo.position, controladorDisparo.rotation);
    }
}
