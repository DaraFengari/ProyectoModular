using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bv : MonoBehaviour
{
    private Slider slider;

    public void InicializarBarraDeVida(int cantidadVida)
    {
        slider.maxValue = cantidadVida;
        CambiarVidaActual(cantidadVida);
    }

    public void CambiarVidaActual(int cantidadVida)
    {
        slider.value = cantidadVida;
    }

    private void Start()
    {
        slider = GetComponent<Slider>();
    }
}

