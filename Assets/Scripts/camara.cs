using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camara : MonoBehaviour
{
    public Vector2 minimo;
    public Vector2 maximo;
    public float suavizado;
    Vector2 velocity;

    //update is called once per frame
    private void FixedUpdate()
    {
        Vector2 objPosition = GameObject.FindWithTag("Player").GetComponent<Transform>().position;
        float posX = Mathf.SmoothDamp(transform.position.x, objPosition.x, ref velocity.x, suavizado);
        float posY = Mathf.SmoothDamp(transform.position.y, objPosition.y, ref velocity.y, suavizado);

        transform.position = new Vector3(Mathf.Clamp(posX, minimo.x, maximo.x), Mathf.Clamp(posY, minimo.y, maximo.y), transform.position.z);
    }
}
