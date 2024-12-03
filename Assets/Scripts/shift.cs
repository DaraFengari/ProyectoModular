using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class shift : MonoBehaviour
{
    [SerializeField] private Image imagen1, imagen2;
    [SerializeField] private TextMeshProUGUI nombre1, nombre2;
    private List<int> CakeIndex = new List<int>();
    private bool primeravez = true;

    int index1, index2;

    private list gameManagger;

    private void Start()
    {
        gameManagger = list.Instance;
        Debug.Log("aqui si entro");
        index1=Generanumero(0);
        index2=Generanumero(index1);
        Debug.Log("aqui si entra");
            Debug.Log("aqui si entra2");

        CambiarEscena();
    }

    private int Generanumero(int pol)
    {
        int randomIndex;
        if (primeravez)
        {
            randomIndex = Random.Range(0, 3);
            primeravez = false;
        }
        else
        {
            randomIndex = Random.Range(0, 3);
            if(randomIndex == pol)
            {
                Generanumero(pol);
            }
        }
        return randomIndex;
    }
    private void CambiarEscena()
    {
        imagen1.sprite = gameManagger.tiendas[index1].imagen;
        nombre1.text = gameManagger.tiendas[index1].nombre;

        imagen2.sprite = gameManagger.tiendas[index2].imagen;
        nombre2.text = gameManagger.tiendas[index2].nombre;
    }

    public void Warp1()
    {
        gameObject.GetComponent<warp>().tienda1(gameManagger.tiendas[index1].escena);
    }

    public void Warp2()
    {
        gameObject.GetComponent<warp>().tienda1(gameManagger.tiendas[index2].escena);
        
    }
}
