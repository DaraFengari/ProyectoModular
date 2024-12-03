using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atkbomb : MonoBehaviour
{
    [SerializeField] private Transform controladorDisparo;
    [SerializeField] private GameObject bomb;

    private void OnEnable()
    {
        Vector3 north= new Vector3(controladorDisparo.position.x + 25,controladorDisparo.position.y);
        Vector3 east = new Vector3(controladorDisparo.position.x, controladorDisparo.position.y + 25);
        Vector3 west = new Vector3(controladorDisparo.position.x, controladorDisparo.position.y - 25);
        Vector3 sout = new Vector3(controladorDisparo.position.x - 25, controladorDisparo.position.y);
        Instantiate(bomb, north, controladorDisparo.rotation);
        Instantiate(bomb, east, controladorDisparo.rotation);
        Instantiate(bomb, west, controladorDisparo.rotation);
        Instantiate(bomb, sout, controladorDisparo.rotation);
        enabled = false;
    }
}
