using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Combate : MonoBehaviour
{
    public int maximoVida, maximoArma, vida, armadura;
    public Bv bv;
    public Ba ba;
    public TMP_Text texvidmax, texdefmax, texvidact, texdefact;
    float timer;
    public Personajes personaje; // Cambiado a Personajes

    private void Awake()
    {
        bv = GameObject.FindWithTag("ui").GetComponent<Canvas>().GetComponentInChildren<Bv>();
        ba = GameObject.FindWithTag("ui").GetComponent<Canvas>().GetComponentInChildren<Ba>();
        texvidmax = GameObject.Find("Maxv").GetComponent<TMP_Text>();
        texdefmax = GameObject.Find("Maxa").GetComponent<TMP_Text>();
        texvidact = GameObject.Find("Actv").GetComponent<TMP_Text>();
        texdefact = GameObject.Find("Acta").GetComponent<TMP_Text>();
    }
    private void Start()
    {
        // Inicializar las barras con los valores actuales
        InitializeBars();
    }

    public void InitializeBars()
    {
        maximoVida = personaje.vidaMaxima;
        maximoArma = personaje.armaduraMaxima;

        vida = maximoVida;
        armadura = maximoArma;

        bv.InicializarBarraDeVida(vida);
        ba.InicializarBarraDeArma(armadura);

        texvidmax.text = maximoVida.ToString();
        texdefmax.text = maximoArma.ToString();
        texvidact.text = vida.ToString();
        texdefact.text = armadura.ToString();
    }

    public void TomarDaño(int daño)
    {
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
        }
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

