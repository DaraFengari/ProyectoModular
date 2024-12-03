using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class burb : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private int daño;
    private Vector3 initialDirection; // Almacena la dirección inicial
    private Quaternion originalRotation; // Almacena la rotación original del objeto

    // Start is called before the first frame update
    void Start()
    {
        // Obtén la posición del clic en el plano de la cámara
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Calcula la dirección hacia la posición del clic
        initialDirection = (mousePosition - transform.position).normalized; // Normaliza la dirección
        // Guarda la rotación original del objeto
        originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        // Mueve el objeto en la dirección inicial
        transform.position += initialDirection * Time.deltaTime * velocidad; // Ajusta la velocidad según sea necesario
    }



    private void OnCollisionEnter2D(Collision2D other)
    {
        
        // Verifica que el objeto colisionado tenga la etiqueta "Enemigo", "Player" o "Borde"
        if (other.gameObject.CompareTag("Enemigo")|| other.gameObject.CompareTag("Borde"))
        {
            Debug.Log("colision detectada");
            // Cambia la dirección del objeto al azar entre los ángulos especificados
            CambiarDireccion();

            // Acelera la velocidad del objeto en 1.5
            velocidad += velocidad * 0.3f;
        }

        // Verifica que el objeto colisionado tenga la etiqueta "Enemigo"
        if (other.gameObject.CompareTag("Enemigo"))
        {
            // Obtén el componente Enemig del objeto colisionado
            Enemig enemigo = other.gameObject.GetComponent<Enemig>();

            // Verifica que el componente Enemig no sea nulo
            if (enemigo != null)
            {
                // Aplica el daño al enemigo
                enemigo.TomarDaño(daño);
            }
        }
    }

    // Función para cambiar la dirección del objeto al azar entre ángulos específicos
    private void CambiarDireccion()
    {
        float[] angles = { 22.5f, 45f, 67.5f, 90f, 112.5f, 135f, 157.5f, 180f };
        // Selecciona un ángulo al azar de la lista
        float newAngle = angles[Random.Range(0, angles.Length)];
        // Calcula la nueva dirección sin cambiar la rotación original
        Vector3 newDirection = Quaternion.AngleAxis(newAngle, Vector3.forward) * originalRotation * Vector3.right;
        // Actualiza la dirección inicial con la nueva dirección
        initialDirection = newDirection.normalized;
    }

    private void OnDestroy()
    {
        GameObject.FindWithTag("Player").GetComponent<Animator>().SetBool("attacking", false);
    }
}

