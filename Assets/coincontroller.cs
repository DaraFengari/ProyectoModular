using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coincontroller : MonoBehaviour
{
    private float tiempoDeEspera = 0.5f;
    private float tiempoUltimaColision;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Time.time - tiempoUltimaColision >= tiempoDeEspera)
            {
                Destroy(gameObject);
                Monedas.moneda.AgregarMonedas(1);
                tiempoUltimaColision = Time.time;
            }
                
            
        }   
    }
    
}
