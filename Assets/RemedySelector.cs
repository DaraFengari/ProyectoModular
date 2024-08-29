using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class RemedySelector : MonoBehaviour
{
    private List<int> RemedyIndex = new List<int>();
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

    private GameMannager gameMannager;
    
    void Start()
    {
        gameMannager = GameMannager.Instance;
        coin = 300;
        while (RemedyIndex.Count < 3)
        {
            int randomIndex = Random.Range(0, 5);
            if (!RemedyIndex.Contains(randomIndex))
            {
                RemedyIndex.Add(randomIndex);
            }
        }
        index1 = RemedyIndex[0];
        index2 = RemedyIndex[1];
        index3 = RemedyIndex[2];
        CambiarEscena();

    }
    private void CambiarEscena()
    {
        imagen1.sprite = gameMannager.remedios[index1].imagen;
        precio1.text = "$" + gameMannager.remedios[index1].precio.ToString();
        description1.text = gameMannager.remedios[index1].descripcion.ToString();

        imagen2.sprite = gameMannager.remedios[index2].imagen;
        precio2.text = "$" + gameMannager.remedios[index2].precio.ToString();
        description2.text = gameMannager.remedios[index2].descripcion.ToString();

        imagen3.sprite = gameMannager.remedios[index3].imagen;
        precio3.text = "$" + gameMannager.remedios[index3].precio.ToString();
        description3.text = gameMannager.remedios[index3].descripcion.ToString();
    }

    public void CompraBoton1()
    {
        Debug.Log("Boton click");
        if (coin >= gameMannager.remedios[index1].precio)
        {
            ComprarRemedio(index1);
        }
        else
        {
            Debug.Log("No tienes suficientes monedas para comprar este remedio.");
        }
    }

    public void CompraBoton2()
    {
        Debug.Log("Boton click");
        if (coin >= gameMannager.remedios[index2].precio)
        {
            ComprarRemedio(index2);
        }
        else
        {
            Debug.Log("No tienes suficientes monedas para comprar este remedio.");
        }
    }

    public void CompraBoton3()
    {
        Debug.Log("Boton click");
        if (coin >= gameMannager.remedios[index3].precio)
        {
            ComprarRemedio(index3);
        }
        else
        {
            Debug.Log("No tienes suficientes monedas para comprar este remedio.");
        }
    }
    public void ComprarRemedio(int indice)
    {
        int remedioPrecio = gameMannager.remedios[indice].precio;
        if (Monedas.Instance.GastarMonedas(remedioPrecio))
        {
            Debug.Log("Pastel comprado: " + gameMannager.remedios[indice].nombre);
        }

    }


}
