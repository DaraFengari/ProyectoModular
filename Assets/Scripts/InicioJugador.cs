using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InicioJugador : MonoBehaviour
{
    private Combate combat;
    private MapManager mapManager; // Añadir referencia al MapManager

    private void Start()
    {
        //Obtén la referencia de MapManager en la escena
        mapManager = FindObjectOfType<MapManager>();

         //Verifica que haya un MapManager y que se haya generado el punto de spawn
        if (mapManager != null && mapManager.playerPlaced)
        {
            int indexJugador = PlayerPrefs.GetInt("JugadorIndex");

            //Usa la posición de spawn generada por el MapManager
            Vector2Int spawnPoint = mapManager.spawnPoint;
            Vector3 spawnPosition = new Vector3(spawnPoint.x, spawnPoint.y, 0); // Asegúrate de usar el eje correcto

            //Instancia el personaje en la posición del spawn point
            GameObject personajeObj = Instantiate(GameManager.Instance.personajes[indexJugador].personajeJugable, spawnPosition, Quaternion.identity);
            combat = personajeObj.GetComponent<Combate>();

            // Asignar el personaje desde GameManager
            combat.personaje = GameManager.Instance.personajes[indexJugador];

            LoadGame();
            combat.InitializeBars(); // Inicializar las barras después de cargar los valores
        }
        else
        {
            Debug.LogError("No se pudo encontrar el MapManager o no se ha generado el punto de spawn.");
        }
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt(combat.personaje.nombre + "_vida", combat.vida);
        PlayerPrefs.SetInt(combat.personaje.nombre + "_armadura", combat.armadura);
        PlayerPrefs.Save();
    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey(combat.personaje.nombre + "_vida"))
        {
            combat.vida = PlayerPrefs.GetInt(combat.personaje.nombre + "_vida");
        }

        if (PlayerPrefs.HasKey(combat.personaje.nombre + "_armadura"))
        {
            combat.armadura = PlayerPrefs.GetInt(combat.personaje.nombre + "_armadura");
        }

        // Asegúrate de que los valores se actualicen después de cargar
        //combat.bv.CambiarVidaActual(combat.vida);
        //combat.ba.CambiarArmaActual(combat.armadura);
        //combat.texvidact.text = combat.vida.ToString();
        //combat.texdefact.text = combat.armadura.ToString();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
