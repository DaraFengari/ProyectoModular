using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CakeComentary : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject cakeComentary;
    //public void Start()
    //{
      //  cakeComentary.SetActive(false);
    //}

    public void OnMouseOver()
    {
        cakeComentary.SetActive(true);
    }

    public void OnMouseExit()
    {
        cakeComentary.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        cakeComentary.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        cakeComentary.SetActive(false);
    }
}
