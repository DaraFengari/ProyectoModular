using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker : MonoBehaviour
{
    public float baseSpeed;
    public bool flee;
    private Vector2 targetPosition;
    private float fleeDuration = 1.0f;

    void Start()
    {
        // Iniciar la primera posición de destino
        Vector2 targetPosition = GameObject.FindWithTag("Player").GetComponent<Transform>().position;
        flee = false;
    }

    void Update()
    {
        // Actualizar la posición de destino del jugador
        Vector2 targetPosition = GameObject.FindWithTag("Player").GetComponent<Transform>().position;

        // Calcular la dirección hacia el jugador
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        float distanceToPlayer = Vector2.Distance(transform.position, targetPosition);

        if(flee==false)
        {
            Vector2 newPosition = (Vector2)transform.position + direction * baseSpeed * Time.deltaTime;
            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
        }
        else
        {
            StartCoroutine(AccelerateForDuration());
            Vector2 fleeDirection = -direction; // Alejarse del jugador
            Vector2 fleePosition = (Vector2)transform.position + fleeDirection * baseSpeed * Time.deltaTime;
            transform.position = new Vector3(fleePosition.x, fleePosition.y, transform.position.z);
        }
        
    }

    IEnumerator AccelerateForDuration()
    {
        yield return new WaitForSeconds(fleeDuration);
        flee = false;
    }
}
