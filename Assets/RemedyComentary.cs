using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RemedyComentary : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject remedyComentary;

    private void OnMouseOver()
    {
        remedyComentary.SetActive(true);
    }
    void OnMouseExit()
    {
        remedyComentary.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        remedyComentary.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        remedyComentary.SetActive(false);
    }

}
