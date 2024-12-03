using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flameatack : MonoBehaviour
{
    public int daño;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
                other.GetComponent<Combate>().TomarDaño(daño);
                //Actualiza el tiempo de la última colisión
        }
    }
}
