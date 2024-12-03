using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InicioJugador : MonoBehaviour
{
    private Combate combat;
    private NewBehaviourScript fast;
    private Monedas okay;
    private void Start()
    {
        okay=GameObject.Find("monedascontroll").GetComponent<Monedas>();
        Debug.Log(okay.pedro);
        int indexJugador = PlayerPrefs.GetInt("JugadorIndex");

        //Instancia el personaje en la posición del spawn point
        GameObject personajeObj = Instantiate(GameManager.Instance.personajes[indexJugador].personajeJugable, transform.position, Quaternion.identity);
        combat = personajeObj.GetComponent<Combate>();
        fast = personajeObj.GetComponent<NewBehaviourScript>();

        // Asignar el personaje desde GameManager
        combat.personaje = GameManager.Instance.personajes[indexJugador];
        
        //SaveGame();
        //LoadGame();
        //combat.InitializeBars(); // Inicializar las barras después de cargar los valores
        if (!okay.pedro)
        {
            combat.vida = combat.personaje.vidaMaxima;
            combat.armadura = combat.personaje.armaduraMaxima;
            combat.maximoArma = combat.armadura;
            combat.maximoVida = combat.vida;
            combat.velocidad = combat.personaje.velocidad;
            fast.speedchange();
            PlayerPrefs.SetInt(combat.personaje.nombre + "_vidamax", combat.vida);
            PlayerPrefs.SetInt(combat.personaje.nombre + "_armaduramax", combat.armadura);
            PlayerPrefs.Save();
            okay.pedro = true;
            Debug.Log("susanita tiene un raton");
        }
        else
        {
            LoadGame();
            fast.speedchange();
            Debug.Log("malditaseafuncionaomemato");
        }
        Debug.Log(okay);

        combat.activasiondeescudo();
    }

    public void SaveGame()
    {
        Debug.Log("guardando");
        PlayerPrefs.SetInt(combat.personaje.nombre + "_vida", combat.vida);
        PlayerPrefs.SetInt(combat.personaje.nombre + "_armadura", combat.armadura);
        PlayerPrefs.SetInt(combat.personaje.nombre + "_velocidad", combat.velocidad);
        PlayerPrefs.Save();
        Debug.Log("Espero que funcione esto");
    }

    public void LoadGame()
    {
        combat.vida = PlayerPrefs.GetInt(combat.personaje.nombre + "_vida");
        Debug.Log(combat.personaje.nombre);
        Debug.Log(combat.vida);
        combat.armadura = PlayerPrefs.GetInt(combat.personaje.nombre + "_armadura");
        Debug.Log(combat.armadura);
        combat.maximoVida = PlayerPrefs.GetInt(combat.personaje.nombre + "_vidamax");
        combat.maximoArma = PlayerPrefs.GetInt(combat.personaje.nombre + "_armaduramax");
        combat.velocidad = PlayerPrefs.GetInt(combat.personaje.nombre + "_velocidad");
        if (PlayerPrefs.HasKey(combat.personaje.nombre + "_vida"))
        {
            
        }

        if (PlayerPrefs.HasKey(combat.personaje.nombre + "_armadura"))
        {
            
        }

        if(PlayerPrefs.HasKey(combat.personaje.nombre + "_velocidad"))
        {
            combat.velocidad = PlayerPrefs.GetInt(combat.personaje.nombre + "_velocidad");
        }

        // Asegúrate de que los valores se actualicen después de cargar
        //combat.bv.CambiarVidaActual(combat.vida);
        //combat.ba.CambiarArmaActual(combat.armadura);
        //combat.texvidact.text = combat.vida.ToString();
        //combat.texdefact.text = combat.armadura.ToString();
    }

    public void reload()
    {
        PlayerPrefs.SetInt(combat.personaje.nombre + "_vidamax", combat.personaje.vidaMaxima);
        PlayerPrefs.SetInt(combat.personaje.nombre + "_armaduramax", combat.personaje.armaduraMaxima);
        PlayerPrefs.SetInt(combat.personaje.nombre + "_velocidad", combat.personaje.velocidad);
        PlayerPrefs.Save();
        okay.pedro = false;
        okay.limpiar();

    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
