using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Monedas : MonoBehaviour
{
    public static Monedas Instance;
    public int cantidadMonedas;
    
    private void Awake()
    {
        if (Monedas.Instance == null)
        {
            Monedas.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AgregarMonedas(int cantidad)
    {
        cantidadMonedas += cantidad;
        Debug.Log("Monedas agregadas: " + cantidad + ". Total: " + cantidadMonedas);
    } 

    public bool GastarMonedas(int cantidad)
    {
        if (cantidadMonedas >= cantidad)
        {
            cantidadMonedas -= cantidad;
            Debug.Log("Monedas gastadas: " + cantidad + ". Total restante: " + cantidadMonedas);
            return true;
        }
        else
        {
            Debug.Log("No hay suficientes monedas para completar la compra.");
            return false;
        }
    }
}
