using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker : MonoBehaviour
{
    public float baseSpeed;
    public bool flee;
    private float fleeDuration = 1.0f;

    void Start()
    {
        // Iniciar la primera posición de destino
        Vector2 targetPosition = GameObject.FindWithTag("Player").GetComponent<Transform>().position;
        flee = false;
    }

    void Update()
    {
        var step= baseSpeed * Time.deltaTime;
        // Actualizar la posición de destino del jugador
        Vector2 targetPosition = GameObject.FindWithTag("Player").GetComponent<Transform>().position;

        if(flee==false)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);
        }
        else
        {
            StartCoroutine(AccelerateForDuration());
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, -step);
        }
        
    }

    private IEnumerator AccelerateForDuration()
    {
        yield return new WaitForSeconds(fleeDuration);
        flee = false;
    }
}
