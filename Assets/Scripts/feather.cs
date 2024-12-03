using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class feather : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private int da�o;
    private Vector3 initialDirection; // Almacena la direcci�n inicial
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

        // Calcula la direcci�n hacia la posici�n del clic
        initialDirection = (mousePosition - transform.position).normalized; // Normaliza la direcci�n
        // Direccion para la rotacion
        Vector3 direction = mousePosition - transform.position;
        // Calcula el �ngulo de rotaci�n en radianes
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // Establece la rotaci�n del objeto hacia la posici�n del clic
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        // Guarda la rotaci�n original del objeto
    }

    // Update is called once per frame
    void Update()
    {
        // Mueve el objeto en la direcci�n inicial
        transform.position += initialDirection * Time.deltaTime * velocidad; // Ajusta la velocidad seg�n sea necesario
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
