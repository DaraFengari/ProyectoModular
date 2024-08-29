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
                timer = 0;
                Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpeM.position, radioGolpe);
                foreach (Collider2D colisionador in objetos)
                {
                    if (colisionador.CompareTag("Player"))
                    {
                        colisionador.transform.GetComponent<Combate>().TomarDaño(1);
                        GetComponent<Seeker>().flee = true;
                    }
                }

            }
    }
}
