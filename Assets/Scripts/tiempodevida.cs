using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tiempodevida : MonoBehaviour
{
    [SerializeField] private float tiempovida;
    void Start()
    {
        Destroy(gameObject,tiempovida);
    }

}
