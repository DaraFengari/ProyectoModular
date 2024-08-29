using System.Collections;
using UnityEngine;

public class disparotang : MonoBehaviour
{
    [SerializeField] private Transform contrDisparo;
    [SerializeField] private GameObject balatnk;

    private int contadorDisparos = 0;
    private int maxDisparos = 5;
    private float tiempoEntreDisparos = 0.1f;
    private float tiempoSuspension = 5f;
    private float timer;

    // Update is called once per frame
    void Update()
    {
        if (contadorDisparos < maxDisparos)
        {
            timer += Time.deltaTime;

            if (timer >= tiempoEntreDisparos)
            {
                timer = 0;
                disparar();
                contadorDisparos++;
            }
        }
        else
        {
            // Entrar en modo de suspensión
            StartCoroutine(SuspenderDisparos());
        }
    }

    private void disparar()
    {
        Instantiate(balatnk, contrDisparo.position, contrDisparo.rotation);
    }

    IEnumerator SuspenderDisparos()
    {
        // Suspender disparos durante el tiempo especificado
        yield return new WaitForSeconds(tiempoSuspension);

        // Reiniciar el contador de disparos y el temporizador
        contadorDisparos = 0;
        timer = 0;
    }
}
