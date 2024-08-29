using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    float timer;
    public GameObject meleePrefab;
    int Cantidad = 0;
    void Update()
    {
        timer += Time.deltaTime;

        if (Cantidad < 3)
        {
            if (timer >= 2f)
            {
                timer = 0;
                float x = Random.Range(-300f, 300f);
                Vector3 position = new Vector3(x, 0, 0);
                Quaternion rotation = new Quaternion();
                Instantiate(meleePrefab, position, rotation);
                Cantidad++;

            }
        }
        
    }
}
