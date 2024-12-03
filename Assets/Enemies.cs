using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    private Vector2 playerPosition; // Posición del jugador como Vector2
    [SerializeField] private float distancia;
    public Vector3 puntoInicial;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        // Obtener la posición del jugador y convertirla a Vector2
        playerPosition = GameObject.FindWithTag("Player").transform.position;

        animator = GetComponent<Animator>();
        puntoInicial = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Actualizar la posición del jugador cada cuadro (si es necesario)
        playerPosition = GameObject.FindWithTag("Player").transform.position;

        // Calcular la distancia usando la posición del jugador
        distancia = Vector2.Distance(transform.position, playerPosition);
        animator.SetFloat("Distancia", distancia);
    }

    public void Girar(Vector3 objetivo)
    {
        if (transform.position.x < objetivo.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}

