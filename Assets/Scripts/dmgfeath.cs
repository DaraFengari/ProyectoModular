using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dmgfeath : MonoBehaviour
{
    [SerializeField] private Transform controladorGolpe;
    [SerializeField] private GameObject pluma;
    [SerializeField] private float delayEntreGolpes = 2.0f; // Tiempo de espera entre golpes en segundos

    private float nextAttackTime = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime && Input.GetMouseButtonDown(0))
        {
            Golpe();
            nextAttackTime = Time.time + delayEntreGolpes; // Actualiza el tiempo del próximo golpe
        }
    }

    private void Golpe()
    {
        Instantiate(pluma, controladorGolpe.position, controladorGolpe.rotation);
    }
}
