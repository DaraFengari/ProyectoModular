using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerRemedies : MonoBehaviour
{
    private int armaduraActual, velocidadActual, vidaActual;
    int indexJugador;
    private GameMannagger gameMannagger;
    private Monedas dutch;

    public void Powers(int var)
    {
        Debug.Log("Esta funcionando en parte");
        gameMannagger = gameObject.GetComponent<GameMannagger>();
        Debug.Log("Avanzaste un poco");
        indexJugador = PlayerPrefs.GetInt("JugadorIndex");
        Debug.Log("ahí vas");
        dutch = GameObject.Find("monedascontroll").GetComponent<Monedas>();
        Debug.Log(indexJugador);
        Debug.Log("No te ilusiones, esto está avanzando");
        switch (var)
        {
            //Aumenta vida pero reduce velocidad
            case 1:
                Debug.Log("camino: " + var);
                // Cargar vida actual desde PlayerPrefs
                Debug.Log(gameMannagger.personajes[indexJugador].nombre);
                vidaActual = PlayerPrefs.GetInt(gameMannagger.personajes[indexJugador].nombre + "_vidamax");
                Debug.Log("vida: " + vidaActual);

                // Aumentar la vida en un 50%
                vidaActual += Mathf.RoundToInt(vidaActual * 0.5f);

                // Guardar nueva vida y armadura en PlayerPrefs usando el nombre del personaje
                PlayerPrefs.SetInt(gameMannagger.personajes[indexJugador].nombre + "_vidamax", vidaActual);

                // Cargar velocidad actual desde PlayerPrefs
                velocidadActual = PlayerPrefs.GetInt(gameMannagger.personajes[indexJugador].nombre + "_velocidad");
                Debug.Log("velocidad antes de la reducción: " + velocidadActual);
                // Reducir la velocidad en un 50%
                velocidadActual -= Mathf.RoundToInt(velocidadActual * 0.5f);
                // Guardar nueva velocidad en PlayerPrefs usando el nombre del personaje
                PlayerPrefs.SetInt(gameMannagger.personajes[indexJugador].nombre + "_velocidad", velocidadActual);

                PlayerPrefs.Save();
                Debug.Log("Vida aumentada. Nueva vida: " + vidaActual);
                Debug.Log("Velocidad reducida. Nueva velocidad: " + velocidadActual);
                break;
            //Obtienes escudo pero disminuyes velocidad
            case 2:
                Debug.Log("camino: " + var);
                dutch.escudado(20);

                // Cargar velocidad actual desde PlayerPrefs
                velocidadActual = PlayerPrefs.GetInt(gameMannagger.personajes[indexJugador].nombre + "_velocidad");
                Debug.Log("velocidad antes de la reducción: " + velocidadActual);
                // Reducir la velocidad en un 50%
                velocidadActual -= Mathf.RoundToInt(velocidadActual * 0.5f);
                // Guardar nueva velocidad en PlayerPrefs usando el nombre del personaje
                PlayerPrefs.SetInt(gameMannagger.personajes[indexJugador].nombre + "_velocidad", velocidadActual);

                PlayerPrefs.Save();
                Debug.Log("Escudo de protección activado.");
                Debug.Log("Velocidad reducida. Nueva velocidad: " + velocidadActual);
                break;
            //Aumenta velocidad pero reduce armadura
            case 3:
                Debug.Log("camino: " + var);
                //Carga vida y el Game Manaager
                velocidadActual = PlayerPrefs.GetInt(gameMannagger.personajes[indexJugador].nombre + "_velocidad");
                
                //Calcula el 50% de velocidad
                velocidadActual += Mathf.RoundToInt(velocidadActual * 0.5f);

                // Guardar nueva vida y armadura en PlayerPrefs usando el nombre del personaje
                PlayerPrefs.SetInt(gameMannagger.personajes[indexJugador].nombre + "_velocidad", velocidadActual);

                // Cargar armadura actual desde PlayerPrefs
                armaduraActual = PlayerPrefs.GetInt(gameMannagger.personajes[indexJugador].nombre + "_armaduramax");
                Debug.Log("armadura antes de la reducción: " + armaduraActual);
                // Reducir la armadura en un 50%
                armaduraActual -= Mathf.RoundToInt(armaduraActual * 0.5f);
                // Guardar nueva armadura en PlayerPrefs usando el nombre del personaje
                PlayerPrefs.SetInt(gameMannagger.personajes[indexJugador].nombre + "_armaduramax", armaduraActual);

                PlayerPrefs.Save();
                Debug.Log("Velocidad aumentada. Nueva velocidad: " + velocidadActual);
                Debug.Log("Armadura reducida. Nueva armadura: " + armaduraActual);
                break;
            //Obtienes escudo pero disminuye armadura
            case 4:
                Debug.Log("camino: " + var);
                dutch.escudado(20);
                Debug.Log("Escudo de protección activado.");

                armaduraActual = PlayerPrefs.GetInt(gameMannagger.personajes[indexJugador].nombre + "_armaduramax");
                Debug.Log("armadura antes de la reducción: " + armaduraActual);
                armaduraActual -= Mathf.RoundToInt(armaduraActual * 0.3f);
                PlayerPrefs.SetInt(gameMannagger.personajes[indexJugador].nombre + "_armaduramax", armaduraActual);

                PlayerPrefs.Save();
                Debug.Log("Armadura reducida. Nueva armadura: " + armaduraActual);
                break;
            //Aumenta armadura pero reduce velocidad
            case 5:
                Debug.Log("camino: " + var);
                armaduraActual = PlayerPrefs.GetInt(gameMannagger.personajes[indexJugador].nombre + "_armaduramax");
                Debug.Log("armadura antes del aumento: " + armaduraActual);
                armaduraActual += Mathf.RoundToInt(armaduraActual * 0.5f);
                PlayerPrefs.SetInt(gameMannagger.personajes[indexJugador].nombre + "_armaduramax", armaduraActual);

                velocidadActual = PlayerPrefs.GetInt(gameMannagger.personajes[indexJugador].nombre + "_velocidad");
                Debug.Log("armadura antes de la reducción: " + armaduraActual);
                // Reducir la armadura en un 50%
                velocidadActual -= Mathf.RoundToInt(velocidadActual * 0.5f);
                // Guardar nueva armadura en PlayerPrefs usando el nombre del personaje
                PlayerPrefs.SetInt(gameMannagger.personajes[indexJugador].nombre + "_armaduramax", armaduraActual);

                PlayerPrefs.Save();
                Debug.Log("Armadura aumentada. Nueva armadura: " + armaduraActual);
                Debug.Log("Velocidad reducida. Nueva velocidad: " + velocidadActual);
                break;
        }

    }
}
