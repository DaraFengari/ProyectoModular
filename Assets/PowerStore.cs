using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PowerStore : MonoBehaviour
{
    private int armaduraActual, velocidadActual, vidaActual;
    int indexJugador;
    private GameManaageer gameManaageer;
    private Monedas dutch;

    public void Poderes(int var)
    {
        Debug.Log("vas por buen camino 2");
        gameManaageer = gameObject.GetComponent<GameManaageer>();
        Debug.Log("vas por buen camino I");
        indexJugador = PlayerPrefs.GetInt("JugadorIndex");
        Debug.Log("vas por buen camino II");
        dutch = GameObject.Find("monedascontroll").GetComponent<Monedas>();
        Debug.Log("vas por buen camino III");
        Debug.Log(indexJugador);
        Debug.Log("vas por buen camino IV");

        switch (var)
        {
            //Aumenta vida y armadura
            case 1:
                Debug.Log("camino: " + var);
                vidaActual = PlayerPrefs.GetInt(gameManaageer.personajes[indexJugador].nombre + "_vidamax");
                Debug.Log("vida: " + vidaActual);
                vidaActual += Mathf.RoundToInt(vidaActual * 0.5f);
                PlayerPrefs.SetInt(gameManaageer.personajes[indexJugador].nombre + "_vidamax", vidaActual);

                armaduraActual = PlayerPrefs.GetInt(gameManaageer.personajes[indexJugador].nombre + "_armaduramax");
                armaduraActual += Mathf.RoundToInt(armaduraActual * 0.5f);
                PlayerPrefs.SetInt(gameManaageer.personajes[indexJugador].nombre + "_armaduramax", armaduraActual);
                PlayerPrefs.Save();

                Debug.Log("Armadura aumentada. Nueva armadura: " + armaduraActual);
                Debug.Log("Vida aumentada. Nueva vida: " + vidaActual);
                break;
            //Aumenta armadura y velocidad
            case 2:
                Debug.Log("camino: " + var);
                armaduraActual = PlayerPrefs.GetInt(gameManaageer.personajes[indexJugador].nombre + "_armaduramax");
                armaduraActual += Mathf.RoundToInt(armaduraActual * 0.5f);
                PlayerPrefs.SetInt(gameManaageer.personajes[indexJugador].nombre + "_armaduramax", armaduraActual);

                velocidadActual = PlayerPrefs.GetInt(gameManaageer.personajes[indexJugador].nombre + "_velocidad");
                velocidadActual += Mathf.RoundToInt(velocidadActual * 0.5f);
                PlayerPrefs.SetInt(gameManaageer.personajes[indexJugador].nombre + "_velocidad", velocidadActual);

                PlayerPrefs.Save();
                Debug.Log("Velocidad aumentada. Nueva velocidad: " + velocidadActual);
                Debug.Log("Armadura aumentada. Nueva armadura: " + armaduraActual);
                break;
            //Aumenta vida y velocidad
            case 3:
                Debug.Log("camino: " + var);
                vidaActual = PlayerPrefs.GetInt(gameManaageer.personajes[indexJugador].nombre + "_vidamax");
                Debug.Log("vida: " + vidaActual);
                vidaActual += Mathf.RoundToInt(vidaActual * 0.5f);
                PlayerPrefs.SetInt(gameManaageer.personajes[indexJugador].nombre + "_vidamax", vidaActual);

                velocidadActual = PlayerPrefs.GetInt(gameManaageer.personajes[indexJugador].nombre + "_velocidad");
                velocidadActual += Mathf.RoundToInt(velocidadActual * 0.5f);
                PlayerPrefs.SetInt(gameManaageer.personajes[indexJugador].nombre + "_velocidad", velocidadActual);

                PlayerPrefs.Save();
                Debug.Log("Vida aumentada. Nueva vida: " + vidaActual);
                Debug.Log("Velocidad aumentada. Nueva velocidad: " + velocidadActual);
                break;
            //Obtienes escudo y aumento de velocidad
            case 4:
                Debug.Log("camino: " + var);
                dutch.escudado(20);
                Debug.Log("Escudo de protección activado.");

                velocidadActual = PlayerPrefs.GetInt(gameManaageer.personajes[indexJugador].nombre + "_velocidad");
                velocidadActual += Mathf.RoundToInt(velocidadActual * 0.5f);
                PlayerPrefs.SetInt(gameManaageer.personajes[indexJugador].nombre + "_velocidad", velocidadActual);

                PlayerPrefs.Save();
                Debug.Log("Velocidad aumentada. Nueva velocidad: " + velocidadActual);
                break;
            //Obtienes escudo y aumento de vida
            case 5:
                Debug.Log("camino: " + var);
                dutch.escudado(20);
                Debug.Log("Escudo de protección activado.");

                vidaActual = PlayerPrefs.GetInt(gameManaageer.personajes[indexJugador].nombre + "_vidamax");
                Debug.Log("vida: " + vidaActual);
                vidaActual += Mathf.RoundToInt(vidaActual * 0.5f);
                PlayerPrefs.SetInt(gameManaageer.personajes[indexJugador].nombre + "_vidamax", vidaActual);
                PlayerPrefs.Save();
                Debug.Log("Vida aumentada. Nueva vida: " + vidaActual);
                break;
        }
    }
}
