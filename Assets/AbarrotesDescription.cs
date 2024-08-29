using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AbarrotesDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject abarrotesDescription;

    private void OnMouseOver()
    {
        abarrotesDescription.SetActive(true);
    }
    void OnMouseExit()
    {
        abarrotesDescription.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        abarrotesDescription.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        abarrotesDescription.SetActive(false);
    }
}
