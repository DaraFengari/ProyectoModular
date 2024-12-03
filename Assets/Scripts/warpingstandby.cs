using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class warpingstandby : MonoBehaviour
{
    float standby = 3;
    private GameObject visualizer;
    private warp jumping;

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
        if(standby <= 0)
        {
            standby = 3;
            jumping = GameObject.FindWithTag("ui").GetComponent<Canvas>().GetComponent<warp>();
            jumping.warping();
        }


    }
}
