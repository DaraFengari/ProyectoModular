using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class burb : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private int da�o;
    private Vector3 initialDirection; // Almacena la direcci�n inicial
    private Quaternion originalRotation; // Almacena la rotaci�n original del objeto

    // Start is called before the first frame update
    void Start()
    {
        // Obt�n la posici�n del clic en el plano de la c�mara
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Calcula la direcci�n hacia la posici�n del clic
        initialDirection = (mousePosition - transform.position).normalized; // Normaliza la direcci�n
        // Guarda la rotaci�n original del objeto
        originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        // Mueve el objeto en la direcci�n inicial
        transform.position += initialDirection * Time.deltaTime * velocidad; // Ajusta la velocidad seg�n sea necesario
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica que el objeto colisionado tenga la etiqueta "Enemigo", "Player" o "Borde"
        if (other.CompareTag("Enemigo")|| other.CompareTag("Borde"))
        {
            // Cambia la direcci�n del objeto al azar entre los �ngulos especificados
            CambiarDireccion();

            // Acelera la velocidad del objeto en 1.5
            velocidad *= 1.5f;
        }

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

    // Funci�n para cambiar la direcci�n del objeto al azar entre �ngulos espec�ficos
    private void CambiarDireccion()
    {
        float[] angles = { 22.5f, 45f, 67.5f, 90f, 112.5f, 135f, 157.5f, 180f };
        // Selecciona un �ngulo al azar de la lista
        float newAngle = angles[Random.Range(0, angles.Length)];
        // Calcula la nueva direcci�n sin cambiar la rotaci�n original
        Vector3 newDirection = Quaternion.AngleAxis(newAngle, Vector3.forward) * originalRotation * Vector3.right;
        // Actualiza la direcci�n inicial con la nueva direcci�n
        initialDirection = newDirection.normalized;
    }
}

