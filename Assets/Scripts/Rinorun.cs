using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rinorun : MonoBehaviour
{
    public float baseSpeed = 48f;
    // Start is called before the first frame update
    void OnEnable()
    {
        gameObject.GetComponent<rinomove>().enabled = false;
        Vector2 targetPosition = GameObject.FindWithTag("Player").GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        float step = baseSpeed * Time.deltaTime;
        Vector2 targetPosition = GameObject.FindWithTag("Player").GetComponent<Transform>().position;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);
        StartCoroutine(runawayplus());
    }

    IEnumerator runawayplus()
    {
        yield return new WaitForSeconds(3);
        gameObject.GetComponent<Rinorun>().enabled = false;
        gameObject.GetComponent<rinomove>().enabled = true;
    }
}
