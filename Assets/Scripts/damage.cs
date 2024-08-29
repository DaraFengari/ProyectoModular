using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damage : MonoBehaviour
{
    [SerializeField] private float tiempoEntreDa�o;

    private float tiempoSiguienteDa�o;
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            tiempoSiguienteDa�o-=Time.deltaTime;
            if(tiempoSiguienteDa�o <= 0)
            {
                other.GetComponent<Combate>().TomarDa�o(1);
                tiempoSiguienteDa�o = tiempoEntreDa�o;
            }
            
        }
    }
}