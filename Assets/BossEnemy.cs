using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    public float baseSpeed = 5.0f;
    public float stopDistance = 2.0f; // Distancia a la que el boss se detendrá cerca del jugador
    public float fleeTime = 5.0f;     // Tiempo de huida
    public float stayNearTime = 3.0f; // Tiempo que se quedará junto al jugador

    private Vector2 targetPosition;
    private bool isFleeing = false;    // Para saber si el boss está huyendo
    private bool isNearPlayer = false; // Para saber si está cerca del jugador

    void Start()
    {
        targetPosition = GameObject.FindWithTag("Player").GetComponent<Transform>().position;
    }

    void Update()
    {
        // Actualizar la posición de destino del jugador
        targetPosition = GameObject.FindWithTag("Player").GetComponent<Transform>().position;

        // Calcular la dirección hacia el jugador
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        float distanceToPlayer = Vector2.Distance(transform.position, targetPosition);

        if (distanceToPlayer > stopDistance && !isFleeing)
        {
            // Si el jefe no está huyendo, se mueve hacia el jugador
            MoveTowardsPlayer(direction);
        }
        else if (distanceToPlayer <= stopDistance && !isNearPlayer)
        {
            // Si está lo suficientemente cerca del jugador, activa la corutina de comportamiento
            StartCoroutine(StayNearPlayer());
        }
    }

    void MoveTowardsPlayer(Vector2 direction)
    {
        // Mover el boss hacia el jugador
        Vector2 newPosition = (Vector2)transform.position + direction * baseSpeed * Time.deltaTime;
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
    }

    void FleeFromPlayer(Vector2 direction)
    {
        // Mover el boss en dirección opuesta al jugador
        Vector2 fleeDirection = -direction;
        Vector2 newPosition = (Vector2)transform.position + fleeDirection * baseSpeed * Time.deltaTime;
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
    }

    IEnumerator StayNearPlayer()
    {
        isNearPlayer = true;
        yield return new WaitForSeconds(stayNearTime); // Quedarse cerca del jugador por 3 segundos

        StartCoroutine(FleeTimer()); // Iniciar la huida después de estar cerca
    }

    IEnumerator FleeTimer()
    {
        isFleeing = true;
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

        float timer = fleeTime;
        while (timer > 0)
        {
            FleeFromPlayer(direction);
            timer -= Time.deltaTime;
            yield return null; // Esperar hasta la siguiente actualización de frame
        }

        isFleeing = false; // Después de huir, volver a perseguir al jugador
        isNearPlayer = false;
    }
}

