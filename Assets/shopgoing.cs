using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class shopgoing : MonoBehaviour
{
    float standby = 3;
    private GameObject visualizer;
    private GameObject jumping;

    private void OnDisable()
    {
        standby = 3;
    }

    private void OnEnable()
    {
        visualizer = GameObject.Find("Timer");
    }
    // Update is called once per frame
    void Update()
    {

        standby -= Time.deltaTime;
        visualizer.GetComponent<TextMeshProUGUI>().text = "0" + standby.ToString("f0");
        if (standby <= 0)
        {
            standby = 3;
            Debug.Log("hasta aqui si llega");
            jumping = GameObject.Find("bifurcar");
            jumping.SetActive(true);
            Debug.Log("no se aqui");
        }


    }

}
