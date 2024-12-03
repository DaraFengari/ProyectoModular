using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tail : MonoBehaviour
{
    [SerializeField] private int da�o;
    [SerializeField] private float tiempoDeEspera = 0.5f; // Tiempo de espera en segundos

    private Transform  playerTransform;
    private GameObject player;
    private bool booling;
    private float tiempoUltimaColision;

    private void Start()
    {

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        transform.SetParent(playerTransform);
        tiempoUltimaColision = -tiempoDeEspera; // Inicializa el tiempo de la �ltima colisi�n para permitir el primer da�o inmediatamente.
    }

    private void Update()
    {
        
        transform.position = playerTransform.position;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Verifica que haya pasado suficiente tiempo desde la �ltima colisi�n
        if (Time.time - tiempoUltimaColision >= tiempoDeEspera)
        {
           

            if (other.gameObject.CompareTag("Enemigo"))
            {
                Enemig enemigo = other.gameObject.GetComponent<Enemig>();
                if (enemigo != null)
                {
                    enemigo.TomarDa�o(da�o);
                }
            }
            
            if (other.gameObject.CompareTag("ammo"))
            {
                print("Collider de bala");
                Balas bal = other.gameObject.GetComponent<Balas>();
                if (bal != null)
                {
                    bal.bounce = true;
                    print("True enviado");
                }
            }

            // Actualiza el tiempo de la �ltima colisi�n
            tiempoUltimaColision = Time.time;
        }
    }

    private void OnDestroy()
    {
        GameObject.FindWithTag("Player").GetComponent<Animator>().SetBool("attacking", false);
    }
}
