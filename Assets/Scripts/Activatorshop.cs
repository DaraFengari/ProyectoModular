using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activatorshop : MonoBehaviour
{
    private GameObject ravioli;

    private void Start()
    {
        ravioli = GameObject.Find("Timer");
        ravioli.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("colision detectada");
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("colision detectada");
            ravioli.SetActive(true);
            gameObject.GetComponent<shopgoing>().enabled = true;
        }

    }

    private void OnCollisionExit2D(Collision2D other)
    {
        Debug.Log("colision detectada");
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("colision detectada");
            ravioli.SetActive(false);
            gameObject.GetComponent<shopgoing>().enabled = false;
        }

    }
}
