using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dmg : MonoBehaviour
{
    [SerializeField] private Transform controladorGolpe;
    [SerializeField] private GameObject leng;
    [SerializeField] private float delayEntreGolpes = 2.0f; // Tiempo de espera entre golpes en segundos
    private float nextAttackTime = 0f;

    private void Update()
    {
        if (Time.time >= nextAttackTime && Input.GetMouseButtonDown(0))
        {
            Golpe();
            nextAttackTime = Time.time + delayEntreGolpes; // Actualiza el tiempo del próximo golpe
        }
    }
    private void Golpe()
    {
        Instantiate(leng, controladorGolpe.position, controladorGolpe.rotation);
    }

}
