using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GrocerySelector : MonoBehaviour
{
    private List<int> GroceryIndex = new List<int>();
    private PowerStore chuies;
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
    private GameMaanager gameMaanager;
    void Start()
    {
        gameMaanager = GameMaanager.Instance;
        chuies = GameObject.Find("Store").GetComponent<PowerStore>();
        coin = GameObject.Find("monedascontroll").GetComponent<Monedas>().cantidadMonedas;
        while (GroceryIndex.Count < 3)
        {
            int randomIndex = Random.Range(0, 5);
            if (!GroceryIndex.Contains(randomIndex))
            {
                GroceryIndex.Add(randomIndex);
            }
        }
        index1 = GroceryIndex[0];
        index2 = GroceryIndex[1];
        index3 = GroceryIndex[2];
        CambiarEscena();
    }

    private void CambiarEscena()
    {
        imagen1.sprite = gameMaanager.grocery[index1].imagen;
        precio1.text = "$" + gameMaanager.grocery[index1].precio.ToString();
        description1.text = gameMaanager.grocery[index1].descripcion.ToString();

        imagen2.sprite = gameMaanager.grocery[index2].imagen;
        precio2.text = "$" + gameMaanager.grocery[index2].precio.ToString();
        description2.text = gameMaanager.grocery[index2].descripcion.ToString();

        imagen3.sprite = gameMaanager.grocery[index3].imagen;
        precio3.text = "$" + gameMaanager.grocery[index3].precio.ToString();
        description3.text = gameMaanager.grocery[index3].descripcion.ToString();
    }
    public void ComprarBoton1()
    {
        Debug.Log("Boton click");
        if (coin >= gameMaanager.grocery[index1].precio)
        {
            ComprarGrocery(index1);
        }
        else
        {
            Debug.Log("No tienes suficientes monedas para comprar este remedio.");
        }
    }

    public void ComprarBoton2()
    {
        Debug.Log("Boton click");
        if (coin >= gameMaanager.grocery[index2].precio)
        {
            ComprarGrocery(index2);
        }
        else
        {
            Debug.Log("No tienes suficientes monedas para comprar este remedio.");
        }
    }
    public void ComprarBoton3()
    {
        Debug.Log("Boton click");
        if (coin >= gameMaanager.grocery[index3].precio)
        {
            ComprarGrocery(index3);
        }
        else
        {
            Debug.Log("No tienes suficientes monedas para comprar este remedio.");
        }
    }
    public void ComprarGrocery(int indice)
    {
        int tiendaPrecio = gameMaanager.grocery[indice].precio;
        Monedas.moneda.GastarMonedas(tiendaPrecio);
        coin = GameObject.Find("monedascontroll").GetComponent<Monedas>().cantidadMonedas;

        Debug.Log("Producto comprado: " + gameMaanager.grocery[indice].nombre);
        chuies.Poderes(indice+1);

    }


}
