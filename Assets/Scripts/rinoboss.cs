using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rinoboss : MonoBehaviour
{
    private enum CombatState { Tankattack, BombAttack }
    private CombatState currentState = CombatState.Tankattack;

    [Header("Tank Attack Settings")]
    [SerializeField] private GameObject balatnk;
    [SerializeField] private GameObject warper, explosion;
    private int contadorDisparos = 0;
    private int maxDisparos = 6;
    private float tiempoEntreDisparos = 0.6f;
    private float timer;

    [Header("Bomb Attack Settings")]
    [SerializeField] private Transform controladorDisparo;
    [SerializeField] private GameObject bomb;
    private bool switcher, first = true;
    // Start is called before the first frame update

    private float switchTimer = 0f; // Timer para alternar entre ataques
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        // Gestiona el temporizador de cambio de estado
        switchTimer += Time.deltaTime;
        if(switchTimer > 3f)
        {
            gameObject.GetComponent<Rinorun>().enabled = true;
        }
        if (switchTimer >= 5f)
        {
            switchTimer = 0f;
            ToggleCombatState();
        }

        // Ejecución del estado actual
        switch (currentState)
        {
            case CombatState.Tankattack:
                gameObject.GetComponent<Animator>().SetBool("attack", false);
                first = true;
                Tank();
                break;

            case CombatState.BombAttack:
                gameObject.GetComponent<Animator>().SetBool("attack", true);
                contadorDisparos = 0;
                BombAttack();
                break;
        }

    }

    private void ToggleCombatState()
    {
        switcher = !switcher;
        currentState = switcher ? CombatState.BombAttack : CombatState.Tankattack;
    }

    private void Tank()
    {

        if (contadorDisparos < maxDisparos)
        {
            timer += Time.deltaTime;

            if (timer >= tiempoEntreDisparos)
            {
                timer = 0;
                Disparar();
                contadorDisparos++;
            }
        }
    }

    private void Disparar()
    {
        Instantiate(balatnk, controladorDisparo.position, controladorDisparo.rotation);
    }

    private void BombAttack()
    {
        if (first)
        {
            Vector3 north = new Vector3(controladorDisparo.position.x + 25, controladorDisparo.position.y);
            Vector3 east = new Vector3(controladorDisparo.position.x, controladorDisparo.position.y + 25);
            Vector3 west = new Vector3(controladorDisparo.position.x, controladorDisparo.position.y - 25);
            Vector3 sout = new Vector3(controladorDisparo.position.x - 25, controladorDisparo.position.y);
            Instantiate(bomb, north, controladorDisparo.rotation);
            Instantiate(bomb, east, controladorDisparo.rotation);
            Instantiate(bomb, west, controladorDisparo.rotation);
            Instantiate(bomb, sout, controladorDisparo.rotation);
            gameObject.GetComponent<rinomove>().runaway = true;
            first = false;
        }
    }

    private void OnDestroy()
    {
        Instantiate(explosion, controladorDisparo.position, controladorDisparo.rotation);
        Instantiate(warper, controladorDisparo.position, controladorDisparo.rotation);
    }
}
