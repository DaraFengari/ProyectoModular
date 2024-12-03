using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rinomove : MonoBehaviour
{
    public float baseSpeed;
    public bool runaway = false;
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
        var run = 2*baseSpeed * Time.deltaTime;
        // Actualizar la posición de destino del jugador
        Vector2 targetPosition = GameObject.FindWithTag("Player").GetComponent<Transform>().position;
        if (!runaway)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);
        }
        else
        {
            StartCoroutine(runawayplus());
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, -run);
        }

    }

    IEnumerator runawayplus()
    {
        yield return new WaitForSeconds(2);
        runaway = false;
    }
}
