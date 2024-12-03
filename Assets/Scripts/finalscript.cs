using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalscript : MonoBehaviour
{
    private bool final = false;
    private float temporizador=0;
    public event EventHandler MuerteJugador;
    private GameObject ultimocodigo;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("colision detectada");
            ultimocodigo = GameObject.FindWithTag("Player");
            ultimocodigo.GetComponent<NewBehaviourScript>().enabled = false;
            ultimocodigo.GetComponent<Animator>().SetBool("moving", false);

            final = true;
        }
        
    }

    private void Update()
    {
        if (final)
        {
            Debug.Log("ya entro aqui");
            temporizador += Time.deltaTime;
            if (temporizador >= 3 ) {
                Debug.Log("ahora vamos aqui");
                MuerteJugador?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
