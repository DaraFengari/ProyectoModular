using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    float speed;
    // Start is called before the first frame update
    public void speedchange()
    {
        speed = gameObject.GetComponent<Combate>().velocidad;
        //speedset(30);
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey("d"))
        {
            moveX += speed * Time.deltaTime;
            if (gameObject.GetComponent<SpriteRenderer>().flipX)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }   
        }

        if (Input.GetKey("a"))
        {
            moveX -= speed * Time.deltaTime;
            if(!gameObject.GetComponent<SpriteRenderer>().flipX)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            
        }

        if (Input.GetKey("w"))
        {
            moveY += speed * Time.deltaTime;
        }

        if (Input.GetKey("s"))
        {
            moveY -= speed * Time.deltaTime;
        }

        if (moveX != 0 || moveY != 0)
        {
            gameObject.transform.Translate(moveX, moveY, 0);
            gameObject.GetComponent<Animator>().SetBool("moving", true);
            gameObject.GetComponent<Animator>().GetBool("moving");
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("moving", false);
        }
    }

    public void speedset(float acc)
    {
        speed = acc;
    }
}
