using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class Monedas : MonoBehaviour
{
    private TMP_Text textomonedas;
    public static Monedas moneda;
    public int cantidadMonedas,cantidadescudo,canthab,puntaje;
    public float min= 0, sec= 0, hour= 0;
    public int enemigosVencidos,visitiendas=0;

    public bool pedro = false, contar = true, nokill=true;
    public bool escudo = false;
    
    private void Awake()
    {
        if (moneda == null)
        {
            moneda = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        } 
    }

    public void limpiar()
    {
        cantidadMonedas = 0;
        enemigosVencidos = 0;
        canthab = 0;
        sec = 0;
        hour = 0;
        min = 0;
        nokill = true;
        puntaje = 0;
        visitiendas = 0;
        contar = true;
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

    private void Update()
    {
        if(textomonedas == null)
        {
            textomonedas = GameObject.Find("Scoretext").GetComponent<TMP_Text>();
            textomonedas.text = cantidadMonedas.ToString();
        }
        textomonedas.text = cantidadMonedas.ToString();

        if (contar)
        {
            sec += Time.deltaTime;
            if (sec >= 60)
            {
                sec = 0;
                min++;
                if (min >= 60)
                {
                    min = 0;
                    hour++;
                }
            }
        }
        
    }

    public void EnemigosDerrotados(int cantidad)
    {
        enemigosVencidos += cantidad;
        Debug.Log("Enemigos vencidos: " + cantidad);
        nokill = false;
    }

    public void habitacionescomp()
    {
        canthab++;
    }

    public void escudado(int nvescudo)
    {
        escudo = true;
        cantidadescudo = nvescudo;
    }

    public void calculatescore()
    {
        puntaje = (canthab * 38) + (enemigosVencidos * 50);
        if (nokill) { puntaje += canthab * 00; }
    }
}
