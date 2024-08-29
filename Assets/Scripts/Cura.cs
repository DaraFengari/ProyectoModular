using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuraA : MonoBehaviour
{
    Combate combate;
    float timer;

    private void Start()
    {
        combate = GameObject.FindGameObjectWithTag("Player").GetComponent<Combate>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 4f)
        {
            timer = 0;
            combate.Curar(1);
        }
    }
}
