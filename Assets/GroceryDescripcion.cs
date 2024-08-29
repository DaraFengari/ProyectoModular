using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GroceryDescripcion : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject groceryDescripcion;

    private void OnMouseOver()
    {
        groceryDescripcion.SetActive(true);
    }
    void OnMouseExit()
    {
        groceryDescripcion.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        groceryDescripcion.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        groceryDescripcion.SetActive(false);
    }
}
