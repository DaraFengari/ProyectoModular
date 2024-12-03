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
            gameObject.GetComponent<Animator>().SetBool("attacking", true);
            Golpe();
            nextAttackTime = Time.time + delayEntreGolpes; // Actualiza el tiempo del próximo golpe
            //Debug.Log("so it goes");
            StartCoroutine(pacifism());
        }
    }

    private void Golpe()
    {
        Instantiate(pluma, controladorGolpe.position, controladorGolpe.rotation);
    }

    private IEnumerator pacifism()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<Animator>().SetBool("attacking", false);
    }
}
