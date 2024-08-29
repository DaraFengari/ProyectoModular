using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GroceryDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject groceryDescription;
    private void OnMouseOver()
    {
        groceryDescription.SetActive(true);
    }
    void OnMouseExit()
    {
        groceryDescription.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        groceryDescription.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        groceryDescription.SetActive(false);
    }
}
