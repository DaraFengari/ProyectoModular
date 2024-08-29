using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
    
public class leng : MonoBehaviour
{
    [SerializeField] private int da�o;
    private Transform playerTransform;

    private void Start()
    {
        // Encuentra el transform del jugador
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // Unir el objeto al jugador
        transform.SetParent(playerTransform);

        // Llama a la funci�n para orientar el objeto hacia la posici�n del clic
        OrientarHaciaClic();
    }

    // Nueva funci�n para orientar el objeto hacia la posici�n del clic
    private void OrientarHaciaClic()
    {
        // Obt�n la posici�n del clic en el plano de la c�mara
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Calcula la direcci�n hacia la posici�n del clic
        Vector3 direction = mousePosition - transform.position;
        // Calcula el �ngulo de rotaci�n en radianes
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // Establece la rotaci�n del objeto hacia la posici�n del clic
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void Update()
    {
        // Puedes agregar l�gica adicional aqu�, por ejemplo, para seguir al jugador.
        // Puedes ajustar la posici�n y la rotaci�n seg�n lo necesites.
        transform.position = playerTransform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica que el objeto colisionado tenga la etiqueta "Enemigo"
        if (other.CompareTag("Enemigo"))
        {
            // Obt�n el componente Enemig del objeto colisionado
            Enemig enemigo = other.GetComponent<Enemig>();

            // Verifica que el componente Enemig no sea nulo
            if (enemigo != null)
            {
                // Aplica el da�o al enemigo
                enemigo.TomarDa�o(da�o);
            }
        }
    }
}
