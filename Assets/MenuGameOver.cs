using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using Unity.VisualScripting;
using TMPro;
using System.Threading;
using System.Timers;

public class MenuGameOver : MonoBehaviour
{
    [SerializeField] private GameObject menuGameOver;
    private Combate combate;
    private Monedas moni;
    private InicioJugador iniciador;
    private Scoreshowe scor;
    private caller cal;



    private void Start()
    {
        combate = GameObject.FindGameObjectWithTag("Player").GetComponent <Combate>();
        combate.MuerteJugador += ActivarMenu;
        iniciador = GameObject.Find("InicioJugador").GetComponent<InicioJugador>();
        moni = GameObject.Find("monedascontroll").GetComponent<Monedas>();
        scor = GameObject.Find("Scripter").GetComponent<Scoreshowe>();
        cal = GameObject.Find("Scripter").GetComponent<caller>();
    }

    public void ActivarMenu(object sender, EventArgs e)
    {
        Debug.Log("ya entro a activacion");
        moni.contar = false;
        moni.calculatescore();
        cal.enabled = true;
        menuGameOver.SetActive(true);
        showtext();
        
    }
    public void Reiniciar(string nombre)
    {
        iniciador.reload();
        moni.contar = true;
        SceneManager.LoadScene(nombre);
    }

    public void MenuInicial(string nombre)
    {
        iniciador.reload();
        SceneManager.LoadScene(nombre);
    }

    public void Salir()
    {
        Application.Quit();
    }

    private void showtext()
    {

        for(int i = 0; i <= 8; i++)
        {
            scor.scorescreen(i);
            
        }
    }
}
