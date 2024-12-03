using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gunslinger : MonoBehaviour
{
    public float baseSpeed = 5.0f;
    public float stopDistance = 2.0f; // Distancia a la que el gunslinger se detendrá


    void Start()
    {
        // Iniciar la primera posición de destino
        Vector2 targetPosition = GameObject.FindWithTag("Player").GetComponent<Transform>().position;
    }

    void Update()
    {
        float step = baseSpeed * Time.deltaTime;
        float away = (float)(1.5 *(baseSpeed * Time.deltaTime));
        

        // Actualizar la posición de destino del jugador
        Vector2 targetPosition = GameObject.FindWithTag("Player").GetComponent<Transform>().position;

        // Calcular la dirección hacia el jugador
        float distanceToPlayer = Vector2.Distance(transform.position, targetPosition);

        if (distanceToPlayer > stopDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);
        }
        else if (distanceToPlayer == stopDistance)
        {
            //transform.position = Vector2.MoveTowards(transform.position, targetPosition, 0);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, -away);
        }
    }

}



