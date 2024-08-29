using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PastelDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject pastelDescription;
    //public void Start()
    //{
      //  pastelDescription.SetActive(false);
    //}

    public void OnMouseOver()
    {
        pastelDescription.SetActive(true);
    }

    public void OnMouseExit()
    {
        pastelDescription.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        pastelDescription.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        pastelDescription.SetActive(false);
    }
}
