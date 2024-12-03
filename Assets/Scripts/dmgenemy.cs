using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class dmgenemy : MonoBehaviour
{
    [SerializeField] private Transform controladorGolpeM;
    [SerializeField] private float radioGolpe;
    float timer;

    private void Update()
    {
        timer += Time.deltaTime;

            if (timer >= 3f)
            {
                gameObject.GetComponent<Animator>().SetBool("attack", true);
                timer = 0;
                Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpeM.position, radioGolpe);
                foreach (Collider2D colisionador in objetos)
                {
                    if (colisionador.CompareTag("Player"))
                    {
                        Debug.Log("entro");
                        colisionador.GetComponent<Combate>().TomarDaño(1);
                        Debug.Log("daño puesto");
                        GetComponent<Seeker>().flee = true;
                    }
                }
                StartCoroutine(returtonormal());
            }
    }

    private IEnumerator returtonormal()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<Animator>().SetBool("attack", false);
    }
}
