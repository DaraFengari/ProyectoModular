using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class feather : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private int daño;
    private Vector3 initialDirection; // Almacena la dirección inicial
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePosition.x < player.transform.position.x)
        {

                GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().flipX = true;
            
        }
        else
        if (mousePosition.x > player.transform.position.x)
        {
            //if(player.GetComponent<Animator>().GetBool("moving") && !player.GetComponent<SpriteRenderer>().flipX){
                GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().flipX = false;
            
            
        }

        // Calcula la dirección hacia la posición del clic
        initialDirection = (mousePosition - transform.position).normalized; // Normaliza la dirección
        // Direccion para la rotacion
        Vector3 direction = mousePosition - transform.position;
        // Calcula el ángulo de rotación en radianes
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // Establece la rotación del objeto hacia la posición del clic
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        // Guarda la rotación original del objeto
    }

    // Update is called once per frame
    void Update()
    {
        // Mueve el objeto en la dirección inicial
        transform.position += initialDirection * Time.deltaTime * velocidad; // Ajusta la velocidad según sea necesario
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
