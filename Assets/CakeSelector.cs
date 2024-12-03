using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PastelSelector : MonoBehaviour
{
    private PowerCakes chui;
    private Monedas monedero;
    private List<int> CakeIndex = new List<int>();
    [SerializeField] private Image imagen1;
    [SerializeField] private Image imagen2;
    [SerializeField] private Image imagen3;
    [SerializeField] private TextMeshProUGUI precio1;
    [SerializeField] private TextMeshProUGUI precio2;
    [SerializeField] private TextMeshProUGUI precio3;
    [SerializeField] private TextMeshProUGUI description1;
    [SerializeField] private TextMeshProUGUI description2;
    [SerializeField] private TextMeshProUGUI description3;


    int index1, index2, index3, coin;

    private GameManagger gameManagger;
    private void Start()
    {
        gameManagger = GameManagger.Instance;
        chui = GameObject.Find("pastelista").GetComponent<PowerCakes>();
        coin = GameObject.Find("monedascontroll").GetComponent<Monedas>().cantidadMonedas;

        while (CakeIndex.Count < 5)
        {
            int randomIndex = Random.Range(0, 5);
            if (!CakeIndex.Contains(randomIndex))
            {
                CakeIndex.Add(randomIndex);
            }
        }
        index1 = CakeIndex[0];
        index2 = CakeIndex[1];
        index3 = CakeIndex[2];
        CambiarEscena();
    }

    private void CambiarEscena()
    {
        //int index = CakeIndex[0];
        imagen1.sprite = gameManagger.pasteles[index1].imagen;
        precio1.text = "$" + gameManagger.pasteles[index1].precio.ToString();
        description1.text = gameManagger.pasteles[index1].descripcion.ToString();

        imagen2.sprite = gameManagger.pasteles[index2].imagen;
        precio2.text = "$" + gameManagger.pasteles[index2].precio.ToString();
        description2.text = gameManagger.pasteles[index2].descripcion.ToString();

        imagen3.sprite = gameManagger.pasteles[index3].imagen;
        precio3.text = "$" + gameManagger.pasteles[index3].precio.ToString();
        description3.text = gameManagger.pasteles[index3].descripcion.ToString();
    }

    public void ComproBoton1()
    {
        if (coin >= gameManagger.pasteles[index1].precio)
        {
          ComprarPastel(index1);
            Debug.Log(index1);
        }
        else
        {
            Debug.Log("No tienes suficientes monedas para comprar este pastel.");
        }
    }

    public void ComproBoton2()
    {
        if (coin >= gameManagger.pasteles[index2].precio)
        {
            ComprarPastel(index2);
            Debug.Log(index2);
        }
        else
        {
            Debug.Log("No tienes suficientes monedas para comprar este pastel.");
        }
    }

    public void ComproBoton3()
    {
        if (coin >= gameManagger.pasteles[index3].precio)
        {
            ComprarPastel(index3);
            Debug.Log(index3);
        }
        else
        {
            Debug.Log("No tienes suficientes monedas para comprar este pastel.");
        }
    }

    public void ComprarPastel(int indice)
    {
        Debug.Log("vas por buen camino 1");
        int pastelPrecio = gameManagger.pasteles[indice].precio;

        Monedas.moneda.GastarMonedas(pastelPrecio);
        coin = GameObject.Find("monedascontroll").GetComponent<Monedas>().cantidadMonedas;
        //{
        Debug.Log("Pastel comprado: " + gameManagger.pasteles[indice].nombre);
        //}
        chui.Poderes(indice+1);

    }
}