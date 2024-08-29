using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RemedioDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject remedioDescription;
    private void OnMouseOver()
    {
        remedioDescription.SetActive(true);
    }
    void OnMouseExit()
    {
        remedioDescription.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        remedioDescription.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        remedioDescription.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
