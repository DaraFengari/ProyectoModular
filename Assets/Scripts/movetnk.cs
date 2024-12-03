using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movetnk : MonoBehaviour
{
    private float timer,baseSpeed=28;

    void Start()
    {
        Vector2 targetPosition = GameObject.FindWithTag("Player").GetComponent<Transform>().position;
    }

    void Update()
    {
        float step = baseSpeed * Time.deltaTime;
        Vector2 targetPosition = GameObject.FindWithTag("Player").GetComponent<Transform>().position;
        // Actualizar la posición de destino del jugador

        // Calcular la dirección hacia el jugador
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);

        // Manejar la aceleración
        timer += Time.deltaTime;
        if (timer >= 5)
        {
            gameObject.GetComponent<Animator>().SetBool("Runing", true);
        }
    }

    private void OnDisable()
    {
        timer = 0;
    }
}

