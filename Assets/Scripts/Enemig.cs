using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemig : MonoBehaviour
{
    [SerializeField] private float vida;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TomarDaño(float daño)
    {
        vida -= daño;
        Debug.Log(vida);

        if (vida <= 0)
        {
            Destroy(gameObject);
            Muerte();
        }
    }

    private void Muerte()
    {
        Monedas.moneda.EnemigosDerrotados(1);
    }
}
