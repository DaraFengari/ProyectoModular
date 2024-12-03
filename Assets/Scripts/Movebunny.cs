using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movebunny : MonoBehaviour
{
    public float baseSpeed;
    // Start is called before the first frame update
    void Start()
    {
        Vector2 targetPosition = GameObject.FindWithTag("Player").GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        //counter += Time.deltaTime;
        var step = baseSpeed * Time.deltaTime;
        // Actualizar la posición de destino del jugador
        Vector2 targetPosition = GameObject.FindWithTag("Player").GetComponent<Transform>().position;
        if (Vector3.Distance(targetPosition, transform.position) < 30)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, 5*-step);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);
        }
        
    }
}
