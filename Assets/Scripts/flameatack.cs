using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flameatack : MonoBehaviour
{
    public int da�o;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
                other.GetComponent<Combate>().TomarDa�o(da�o);
                //Actualiza el tiempo de la �ltima colisi�n
        }
    }
}
