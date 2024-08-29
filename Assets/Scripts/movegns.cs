using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunslinger : MonoBehaviour
{
    public float baseSpeed = 5.0f;
    public float stopDistance = 2.0f; // Distancia a la que el gunslinger se detendrá

    private Vector2 targetPosition;

    void Start()
    {
        // Iniciar la primera posición de destino
        Vector2 targetPosition = GameObject.FindWithTag("Player").GetComponent<Transform>().position;
    }

    void Update()
    {
        // Actualizar la posición de destino del jugador
        Vector2 targetPosition = GameObject.FindWithTag("Player").GetComponent<Transform>().position;

        // Calcular la dirección hacia el jugador
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        float distanceToPlayer = Vector2.Distance(transform.position, targetPosition);

        if (distanceToPlayer > stopDistance)
        {
            // Mover el objeto gunslinger
            Vector2 newPosition = (Vector2)transform.position + direction * baseSpeed * Time.deltaTime;
            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
        }
        else if (distanceToPlayer == stopDistance)
        {
            // Permanecer completamente quieto
        }
        else
        {
            // Huir a una velocidad doble de la base
            Vector2 fleeDirection = -direction; // Alejarse del jugador
            Vector2 fleePosition = (Vector2)transform.position + fleeDirection * baseSpeed * Time.deltaTime;
            transform.position = new Vector3(fleePosition.x, fleePosition.y, transform.position.z);
        }
    }
}



