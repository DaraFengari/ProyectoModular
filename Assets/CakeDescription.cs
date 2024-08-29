using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CakeDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject cakeDescription;
    //void Start()
    //{
      //  cakeDescription.SetActive(false);
    //}

    //void OnMouseOver()
    //{
    //cakeDescription.SetActive(true);

    //  Debug.Log("No se puede");
    //}

    private void OnMouseOver()
    {
        cakeDescription.SetActive(true);
    }
    void OnMouseExit()
    {
        cakeDescription.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        cakeDescription.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        cakeDescription.SetActive(false);
    }
}
