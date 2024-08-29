using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ba : MonoBehaviour
{
    private Slider slider;

    public void InicializarBarraDeArma(int cantidadArma)
    {
        slider.maxValue = cantidadArma;
        CambiarArmaActual(cantidadArma);
    }

    public void CambiarArmaActual(int cantidadArma)
    {
        slider.value = cantidadArma;
    }

    private void Start()
    {
        slider = GetComponent<Slider>();
    }
}

