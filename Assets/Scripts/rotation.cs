using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class rotation : MonoBehaviour
{
    private Vector2 player;
    float rot;
    // Update is called once per frame

    private void Start()
    {
        Vector2 player = GameObject.FindWithTag("Player").GetComponent<Transform>().position;

    }

    void Update()
    {
        
        Vector2 player = GameObject.FindWithTag("Player").GetComponent<Transform>().position;

        if (player.x < gameObject.transform.position.x && (gameObject != null))
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if(player.x > gameObject.transform.position.x)
        {
            rot = 180;
            gameObject.transform.rotation = Quaternion.Euler(0, rot, 0);
        }
    }

}

