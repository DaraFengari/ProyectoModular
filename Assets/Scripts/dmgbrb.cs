using System.Collections;
using UnityEngine;

public class dmgbrb : MonoBehaviour
{
    [SerializeField] private Transform controladorGolpe;
    [SerializeField] private GameObject brb;
    [SerializeField] private float delayEntreGolpes = 1.0f; // Tiempo de espera entre golpes en segundos

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
        Instantiate(brb, controladorGolpe.position, controladorGolpe.rotation);
    }
}

