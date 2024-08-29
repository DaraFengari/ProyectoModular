using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movetnk : MonoBehaviour
{
    public float baseSpeed = 5.0f;
    public float tripleSpeedMultiplier = 3.0f; // Factor por el cual se multiplicará la velocidad al acelerar
    public float stopDistance = 5.0f; // Distancia a la que el gunslinger se detendrá

    private Vector2 targetPosition;
    private float accelerationTimer = 0.0f;
    private float accelerationDuration = 1.0f;
    private float accelerationInterval = 8.0f;
    private bool isAccelerating = false;

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

        // Manejar la aceleración
        accelerationTimer += Time.deltaTime;
        if (accelerationTimer >= accelerationInterval)
        {
            accelerationTimer = 0.0f;
            isAccelerating = true;
            StartCoroutine(AccelerateForDuration());
        }

        // Mover el objeto tank
        float currentSpeed = isAccelerating ? baseSpeed * tripleSpeedMultiplier : baseSpeed;
        Vector2 newPosition = (Vector2)transform.position + direction * currentSpeed * Time.deltaTime;
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
    }

    IEnumerator AccelerateForDuration()
    {
        yield return new WaitForSeconds(accelerationDuration);
        isAccelerating = false;
    }
}

