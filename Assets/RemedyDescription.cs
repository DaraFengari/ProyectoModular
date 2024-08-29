using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RemedyDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject remedyDescription;
    private void OnMouseOver()
    {
        remedyDescription.SetActive(true);
    }
    void OnMouseExit()
    {
        remedyDescription.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        remedyDescription.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        remedyDescription.SetActive(false);
    }
}
