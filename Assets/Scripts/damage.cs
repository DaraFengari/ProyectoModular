using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damage : MonoBehaviour
{
    [SerializeField] private float tiempoEntreDaño;

    private float tiempoSiguienteDaño;
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            tiempoSiguienteDaño-=Time.deltaTime;
            if(tiempoSiguienteDaño <= 0)
            {
                other.GetComponent<Combate>().TomarDaño(1);
                tiempoSiguienteDaño = tiempoEntreDaño;
            }
            
        }
    }
}
