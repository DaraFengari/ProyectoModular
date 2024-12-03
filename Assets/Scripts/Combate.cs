using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Combate : MonoBehaviour
{
    public int maximoVida, maximoArma, vida, armadura, velocidad;
    public Bv bv;
    public Ba ba;
    public TMP_Text texvidmax, texdefmax, texvidact, texdefact;
    float timer;
    public InicioJugador cosas;
    public Personajes personaje;
    public event EventHandler MuerteJugador;
    public MenuGameOver gameover;
    private int proteccion;
    public bool escudoActivo = false;
    public Monedas moni;


    private void Awake()
    {
        bv = GameObject.FindWithTag("ui").GetComponent<Canvas>().GetComponentInChildren<Bv>();
        ba = GameObject.FindWithTag("ui").GetComponent<Canvas>().GetComponentInChildren<Ba>();
        texvidmax = GameObject.Find("Actv").GetComponent<TMP_Text>();
        texdefmax = GameObject.Find("Acta").GetComponent<TMP_Text>();
        texvidact = GameObject.Find("Maxv").GetComponent<TMP_Text>();
        texdefact = GameObject.Find("Maxa").GetComponent<TMP_Text>();
        
    }
    private void Start()
    {
        //cosas.LoadGame();
        // Inicializar las barras con los valores actuales
        InitializeBars();
    }

    public void InitializeBars()
    {
        //maximoVida = personaje.vidaMaxima;
        //maximoArma = personaje.armaduraMaxima;

        //vida = maximoVida;
        //armadura = maximoArma;

        bv.InicializarBarraDeVida(maximoVida);
        ba.InicializarBarraDeArma(maximoArma);

        texvidmax.text = maximoVida.ToString();
        texdefmax.text = maximoArma.ToString();
        texvidact.text = vida.ToString();
        texdefact.text = armadura.ToString();
        //cosas.SaveGame();

    }

    public void activasiondeescudo(){
        moni= GameObject.Find("monedascontroll").GetComponent<Monedas>();
        if (moni.escudo)
        {
            escudoActivo = true;
            proteccion = moni.cantidadescudo;

        }
    }

    public void TomarDaño(int daño)
    {
        if (escudoActivo) 
        {
            Debug.Log("entro");
            if (proteccion > daño)
            {
                Debug.Log("escudo on");
                proteccion -= daño;
                moni.cantidadescudo = proteccion;
                Debug.Log("Daño absorbido por el escudo. Puntos de escudo restantes: " + proteccion);
            }
            else
            {
                escudoActivo = false;
            }
        }
        else
        {
            Debug.Log("entro a daño");
            if (armadura > 0)
            {
                armadura -= daño;
                ba.CambiarArmaActual(armadura);
                texdefact.text = armadura.ToString();
            }
            else
            {
                vida -= daño;
            }
            bv.CambiarVidaActual(vida);
            texvidact.text = vida.ToString();
            if (vida <= 0)
            {
                Destroy(gameObject);
                MuerteJugador?.Invoke(this, EventArgs.Empty);
            }
            escudoActivo = false;
        }

    }


    public void ActivarEscudo(int var)
    {
        escudoActivo = true;
        proteccion = var;
        Debug.Log("Escudo activado con " + var + " puntos.");
    }

    public void Curar(int curacion)
    {
        if ((vida + curacion) > maximoVida)
        {
            vida = maximoVida;
        }
        else
        {
            vida += curacion;
        }
        ba.CambiarArmaActual(armadura);
        bv.CambiarVidaActual(vida);
        texdefact.text = armadura.ToString();
        texvidact.text = vida.ToString();
    }

    void Update()
    {
        ba.CambiarArmaActual(armadura);
        bv.CambiarVidaActual(vida);
        texdefact.text = armadura.ToString();
        texvidact.text = vida.ToString();

        timer += Time.deltaTime;

        if (armadura < maximoArma)
        {
            if (timer >= 4f)
            {
                timer = 0;
                armadura++;
                texdefact.text = armadura.ToString();
            }
        }
    }

}

