using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class movebomber : MonoBehaviour
{
    // Start is called before the first frame update
    public float baseSpeed;
    float increm;
    private bool runaway;
    private float escape = 4.0f;

    void Start()
    {
        // Iniciar la primera posición de destino
        Vector2 targetPosition = GameObject.FindWithTag("Player").GetComponent<Transform>().position;
        runaway = false;
    }

    // Update is called once per frame
    void Update()
    {
        increm += Time.deltaTime;
        var step = baseSpeed * Time.deltaTime;
        // Actualizar la posición de destino del jugador
        Vector2 targetPosition = GameObject.FindWithTag("Player").GetComponent<Transform>().position;
        if (!runaway)
        {
            if (increm <= 5)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);
            }
            else
            {
                increm = 0;
                gameObject.GetComponent<atkbomb>().enabled = true;
                runaway=true;
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        else
        {
            StartCoroutine(runawayplus());
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, -step);
        }
        
    }

    IEnumerator runawayplus()
    {
        yield return new WaitForSeconds(escape);
        runaway = false;
        gameObject.GetComponent<SpriteRenderer>().flipX = true;
    }
}
