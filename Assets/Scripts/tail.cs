using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tail : MonoBehaviour
{
    [SerializeField] private int daño;
    [SerializeField] private float tiempoDeEspera = 0.5f; // Tiempo de espera en segundos

    private Transform playerTransform;
    private float tiempoUltimaColision;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        transform.SetParent(playerTransform);
        tiempoUltimaColision = -tiempoDeEspera; // Inicializa el tiempo de la última colisión para permitir el primer daño inmediatamente.
    }

    private void Update()
    {
        transform.position = playerTransform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica que haya pasado suficiente tiempo desde la última colisión
        if (Time.time - tiempoUltimaColision >= tiempoDeEspera)
        {
            print("Colisión correcta");

            if (other.CompareTag("Enemigo"))
            {
                Enemig enemigo = other.GetComponent<Enemig>();
                if (enemigo != null)
                {
                    enemigo.TomarDaño(daño);
                }
            }
            else if (other.CompareTag("ammo"))
            {
                print("Collider de bala");
                Balas bal = other.GetComponent<Balas>();
                if (bal != null)
                {
                    bal.bounce = true;
                    print("True enviado");
                }
            }

            // Actualiza el tiempo de la última colisión
            tiempoUltimaColision = Time.time;
        }
    }
}
