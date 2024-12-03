using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class atkflamer : MonoBehaviour
{
    [SerializeField] private Transform controladorDisparo;
    [SerializeField] private GameObject Fire;
    private float regreso;

    private void OnEnable()
    {
        if(GameObject.FindWithTag("Player").GetComponent<Transform>().position.x > gameObject.transform.position.x)
            Instantiate(Fire, controladorDisparo.position, controladorDisparo.rotation= Quaternion.Euler(0, 0, 0));
        else
        {
            Instantiate(Fire, controladorDisparo.position, controladorDisparo.rotation = Quaternion.Euler(0, 180, 0));
        }
    }


    void Update()
    {
        regreso += Time.deltaTime;
        if (regreso >= 3)
        {
            regreso = 0;
            //gameObject.GetComponent<moveflame>().enabled = true;
            gameObject.GetComponent<atkflamer>().enabled = false;
            gameObject.GetComponent<rotation>().enabled = true;
        }
    }
}
