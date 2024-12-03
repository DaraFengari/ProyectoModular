using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class runtank : MonoBehaviour
{
    public float baseSpeed = 48f;
    float timer;
    // Start is called before the first frame update
    void OnEnable()
    {
        Vector2 targetPosition = GameObject.FindWithTag("Player").GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        float step = baseSpeed * Time.deltaTime;
        timer += Time.deltaTime;
        Vector2 targetPosition = GameObject.FindWithTag("Player").GetComponent<Transform>().position;

        if (timer >= 0.5)
        {
            gameObject.GetComponent<Animator>().SetBool("Runing", false);
        }
        

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);
    }
}
