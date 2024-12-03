using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveflame : MonoBehaviour
{
    // Start is called before the first frame update
    public float baseSpeed;
    float counter;

    void Start()
    {
        // Iniciar la primera posición de destino
        Vector2 targetPosition = GameObject.FindWithTag("Player").GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        var step = baseSpeed * Time.deltaTime;
        // Actualizar la posición de destino del jugador
        Vector2 targetPosition = GameObject.FindWithTag("Player").GetComponent<Transform>().position;

        if(counter <=3 ) {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);
        }
        else
        {
            gameObject.GetComponent<atkflamer>().enabled = true;
            gameObject.GetComponent<Animator>().SetBool("attack", true);
            gameObject.GetComponent<rotation>().enabled = false;
            StartCoroutine(returtonormal());
            
        }
        
    }

    private IEnumerator returtonormal()
    {
        yield return new WaitForSeconds(3f);
        gameObject.GetComponent<Animator>().SetBool("attack", false);
        counter = 0;
    }

}
