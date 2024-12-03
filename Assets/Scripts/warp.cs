using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class warp : MonoBehaviour
{
    public InicioJugador cosas;
    private int cont;
    private GameManaager gameManaager;
    private Monedas mon;



    private void Awake()
    {
       cosas=GameObject.Find("InicioJugador").GetComponent<InicioJugador>();
        mon = GameObject.Find("monedascontroll").GetComponent<Monedas>();
    }

    public void muestra()
    {
        Debug.Log("click");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void warping()
    {
        Debug.Log("click");
        cosas.SaveGame();
        mon.habitacionescomp();
        Debug.Log("Juego guardado. Cambiando a la siguiente escena...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void tiendawarp()
    {
        mon = GameObject.Find("monedascontroll").GetComponent<Monedas>();
        cont = mon.visitiendas;
        switch (cont)
        {
            case 0:
                 string nombre = "MD1-N3";
                SceneManager.LoadScene(nombre);
                mon.visitiendas++;
                break;
            case 1:
                string nombre2 = "mundo2";
                SceneManager.LoadScene(nombre2);
                mon.visitiendas++;
                break;
            case 2:
                string nombre3 = "MD2-N3";
                SceneManager.LoadScene(nombre3);
                mon.visitiendas++;
                break;
            case 3:
                string nombre4 = "MD3-N1";
                SceneManager.LoadScene(nombre4);
                mon.visitiendas++;
                break;
            case 4:
                string nombre5 = "MD3-N3";
                SceneManager.LoadScene(nombre5);
                mon.visitiendas++;
                break;
            default:
                break;
        }
    }

    public void Mundo1(string nombre)
    {
        SceneManager.LoadScene(nombre);
        cosas.SaveGame();
    }

    public void Mundo2(string nombre)
    {
        SceneManager.LoadScene(nombre);
        cosas.SaveGame();
    }

    public void tienda1(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }

    public void tienda2(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }

    public void tienda3(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }
}
