using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class walkerboss : MonoBehaviour
{
    private Animator boss;
    private Vector3 jugador;
    // Start is called before the first frame update
    void Start()
    {
        boss = gameObject.GetComponent<Animator>();
        jugador = GameObject.FindWithTag("Player").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.position.y <= jugador.y)
        {
            boss.SetBool("north", false);
            boss.SetBool("south", true);

        }else if(gameObject.transform.position.y >= jugador.y)
        {
            boss.SetBool("north", true);
            boss.SetBool("south", false);
        }
    }
}
