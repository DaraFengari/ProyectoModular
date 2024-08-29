using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
    
public class leng : MonoBehaviour
{
    [SerializeField] private int daño;
    private Transform playerTransform;

    private void Start()
    {
        // Encuentra el transform del jugador
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // Unir el objeto al jugador
        transform.SetParent(playerTransform);

        // Llama a la función para orientar el objeto hacia la posición del clic
        OrientarHaciaClic();
    }

    // Nueva función para orientar el objeto hacia la posición del clic
    private void OrientarHaciaClic()
    {
        // Obtén la posición del clic en el plano de la cámara
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Calcula la dirección hacia la posición del clic
        Vector3 direction = mousePosition - transform.position;
        // Calcula el ángulo de rotación en radianes
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // Establece la rotación del objeto hacia la posición del clic
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void Update()
    {
        // Puedes agregar lógica adicional aquí, por ejemplo, para seguir al jugador.
        // Puedes ajustar la posición y la rotación según lo necesites.
        transform.position = playerTransform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica que el objeto colisionado tenga la etiqueta "Enemigo"
        if (other.CompareTag("Enemigo"))
        {
            // Obtén el componente Enemig del objeto colisionado
            Enemig enemigo = other.GetComponent<Enemig>();

            // Verifica que el componente Enemig no sea nulo
            if (enemigo != null)
            {
                // Aplica el daño al enemigo
                enemigo.TomarDaño(daño);
            }
        }
    }
}
