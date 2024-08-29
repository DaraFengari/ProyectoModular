using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balas : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private int daño;
    private Vector2 direccionInicial;
    private bool primeraVez = true;
    public bool bounce;
    [SerializeField] private float tiempoDeEspera = 0.5f; // Tiempo de espera en segundos
    private float tiempoUltimaColision;

    private void Start()
    {
        bounce = false;
        // Obtener la posición del jugador como objetivo inicial
        Vector2 objPosition = GameObject.FindWithTag("Player").GetComponent<Transform>().position;

        // Calcular la dirección inicial
        direccionInicial = (objPosition - (Vector2)transform.position).normalized;

        // Orientar la bala hacia el jugador la primera vez
        OrientarHaciaJugador();

        tiempoUltimaColision = -tiempoDeEspera;
    }

    private void Update()
    {
        if (bounce==false)
        {
            // Mover la bala en la dirección inicial
            Vector2 newPosition = (Vector2)transform.position + direccionInicial * velocidad * Time.deltaTime;
            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
        }
        else if (bounce == true)
        {
            Vector2 newPosition = (Vector2)transform.position - direccionInicial * velocidad * Time.deltaTime;
            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
        }
        Vector2 objPosition = GameObject.FindWithTag("Player").GetComponent<Transform>().position;
        print(objPosition.x);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
            print("True enviado");
            if(other.CompareTag("Player"))
            {
                if (Time.time - tiempoUltimaColision >= tiempoDeEspera)
                {
                   print("hit");
                   other.GetComponent<Combate>().TomarDaño(daño);
                    Destroy(gameObject);
                    //Actualiza el tiempo de la última colisión
                    tiempoUltimaColision = Time.time;
                }
            }
    }

    private void OrientarHaciaJugador()
    {
        if (primeraVez)
        {
            float angulo = Mathf.Atan2(direccionInicial.y, direccionInicial.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angulo, Vector3.forward);
            primeraVez = false;
        }
    }
}

