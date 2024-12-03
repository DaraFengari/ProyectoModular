using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dinamitacion : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    // Update is called once per frame
    [SerializeField] private float tiempovida;
    void Start()
    {
        Destroy(gameObject, tiempovida);
    }


    private void OnDestroy()
    {
        Instantiate(explosion, transform.position, transform.rotation);
    }
}
