using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pruebas : MonoBehaviour
{
    [SerializeField] private Transform controladorGolpe;
    [SerializeField] private float distanciaMaxima;
    [SerializeField] private float anchoRectangulo;
    [SerializeField] private float altoRectangulo;
    [SerializeField] private LayerMask capaEnemigo;
    [SerializeField] private float dañoGolpe;

    [SerializeField] private Camera camaraPrincipal;

    private void Start()
    {
        // Asegúrate de asignar la cámara principal desde el Inspector
        if (camaraPrincipal == null)
        {
            camaraPrincipal = FindObjectOfType<Camera>();
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Golpe();
        }
    }

    private void Golpe()
    {
        Vector2 posicionMouse = camaraPrincipal.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direccion = (posicionMouse - (Vector2)controladorGolpe.position).normalized;
        float distancia = Vector2.Distance(controladorGolpe.position, posicionMouse);

        if (distancia <= distanciaMaxima)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(controladorGolpe.position, direccion, distancia, capaEnemigo);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.CompareTag("Enemigo"))
                {
                    hit.collider.GetComponent<Enemig>().TomarDaño(dañoGolpe);
                }
            }

            // Crear objeto rectangular
            Vector2 centroRectangulo = (Vector2)controladorGolpe.position + direccion * distancia / 2;
            GameObject rectangulo = new GameObject("RectanguloGolpe");
            rectangulo.transform.position = centroRectangulo;
            rectangulo.transform.localScale = new Vector3(anchoRectangulo, altoRectangulo, 1);
            rectangulo.AddComponent<BoxCollider2D>();
            Destroy(rectangulo, 0.1f); // Ajusta el tiempo de destrucción según tus necesidades
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(controladorGolpe.position, new Vector3(distanciaMaxima * 2, distanciaMaxima * 2, 1));
    }
}
