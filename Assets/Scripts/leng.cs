using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
    
public class leng : MonoBehaviour
{
    [SerializeField] private int daño;
    private GameObject player;
    private  Transform playerTransform;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePosition.x < player.transform.position.x)
        {
            if(player.GetComponent<Animator>().GetBool("moving") && !player.GetComponent<SpriteRenderer>().flipX)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().flipX = true;
                playerTransform = GameObject.Find("controlador").transform;
            }
            else
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().flipX = true;
                playerTransform = GameObject.Find("controladorleft").transform;
            }
        }
        else
        if (mousePosition.x > player.transform.position.x)
        {
            if (player.GetComponent<Animator>().GetBool("moving") && player.GetComponent<SpriteRenderer>().flipX)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().flipX = false;
                playerTransform = GameObject.Find("controladorleft").transform;
            }
            else {
                GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().flipX = false;
                playerTransform = GameObject.Find("controlador").transform;
            }
        }

        Vector3 direction = mousePosition - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.SetParent(playerTransform);
    }


    private void Update()
    {
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

    private void OnDestroy()
    {
        GameObject.FindWithTag("Player").GetComponent<Animator>().SetBool("attacking", false);
    }
}
