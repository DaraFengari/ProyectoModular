using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GSSpawn : MonoBehaviour
{
    float timer;
    public GameObject gnslinger;
    int Cantidad = 0;
    void Update()
    {
        timer += Time.deltaTime;

        if (Cantidad < 3)
        {
            if (timer >= 2f)
            {
                timer = 0;
                float x = Random.Range(-100f, 100f);
                Vector3 position = new Vector3(x, 0, 0);
                Quaternion rotation = new Quaternion();
                Instantiate(gnslinger, position, rotation);
                Cantidad++;

            }
        }

    }
}