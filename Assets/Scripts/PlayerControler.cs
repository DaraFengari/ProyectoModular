using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        speedset(30);
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey("d"))
        {
            moveX += speed * Time.deltaTime;
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        if (Input.GetKey("a"))
        {
            moveX -= speed * Time.deltaTime;
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
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
