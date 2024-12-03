using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerCakes : MonoBehaviour
{
    
    private int armaduraActual, velocidadActual, vidaActual;
    int indexJugador;

    private GameManaager gameManaager;
    private Monedas dutch;

    public void Poderes(int var)
    {
        Debug.Log("vas por buen camino 2");
        //combat = gameManaager.GetComponent<Combate>();
        gameManaager = gameObject.GetComponent<GameManaager>();
        Debug.Log("vas por buen camino primero");
        indexJugador = PlayerPrefs.GetInt("JugadorIndex");
        Debug.Log("vas por buen camino segundo");
        dutch = GameObject.Find("monedascontroll").GetComponent<Monedas>();
        Debug.Log("vas por buen camino tercero");
        Debug.Log(indexJugador);
        Debug.Log("vas por buen camino 3");
        switch (var)
        {
            case 1:
                Debug.Log("camino: " + var);
                // Cargar vida actual desde PlayerPrefs
                Debug.Log(gameManaager.personajes[indexJugador].nombre);
                vidaActual = PlayerPrefs.GetInt(gameManaager.personajes[indexJugador].nombre + "_vidamax");
                Debug.Log("vida: " + vidaActual);
                // Aumentar la vida en un 20%

                vidaActual += Mathf.RoundToInt(vidaActual * 0.2f);

                // Guardar nueva vida y armadura en PlayerPrefs usando el nombre del personaje
                PlayerPrefs.SetInt(gameManaager.personajes[indexJugador].nombre + "_vidamax", vidaActual);
                PlayerPrefs.Save();
                Debug.Log("Vida aumentada. Nueva vida: " + vidaActual);
                break;
            case 2:
                Debug.Log("camino: " + var);
                //Carga vida y el Game Manaager
                armaduraActual = PlayerPrefs.GetInt(gameManaager.personajes[indexJugador].nombre + "_armaduramax");

                //Calcula el 50% de armaduraActual
                armaduraActual += Mathf.RoundToInt(armaduraActual * 0.5f);

                // Guardar nueva vida y armadura en PlayerPrefs usando el nombre del personaje
                PlayerPrefs.SetInt(gameManaager.personajes[indexJugador].nombre + "_armaduramax", armaduraActual);
                PlayerPrefs.Save();
                Debug.Log("Armadura aumentada. Nueva armadura: " + armaduraActual);
                break;
            case 3:
                Debug.Log("camino: " + var);
                //Carga vida y el Game Manaager
                vidaActual = PlayerPrefs.GetInt(gameManaager.personajes[indexJugador].nombre + "_vidamax");
                
                //Calcula el 50% de vidaActual
                 vidaActual += Mathf.RoundToInt(vidaActual * 0.5f); 

                PlayerPrefs.SetInt(gameManaager.personajes[indexJugador].nombre + "_vidamax", vidaActual);
                PlayerPrefs.Save();
                Debug.Log("Vida aumentada. Nueva vida: " + vidaActual);
                break;
            case 4:
                Debug.Log("camino: " + var);
                dutch.escudado(20);
                Debug.Log("Escudo de protección activado.");
                break;
            case 5:
                Debug.Log("camino: " + var);
                velocidadActual = PlayerPrefs.GetInt(gameManaager.personajes[indexJugador].nombre + "_velocidad");
                velocidadActual += Mathf.RoundToInt(velocidadActual * 0.5f);
                PlayerPrefs.SetInt(gameManaager.personajes[indexJugador].nombre + "_velocidad", velocidadActual);
                PlayerPrefs.Save();
                Debug.Log("Velocidad aumentada. Nueva velocidad: " + velocidadActual);
                break;
        }
        

    }
    
}
