using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Scoreshowe : MonoBehaviour
{
    private Monedas mori;
    [SerializeField] private GameObject rooms, enemy, time, score, sh, se, shour, sp, smin, sseg, slash, slash2;
    public void scorescreen(int x)
    {
        Debug.Log("esto es extraño");
        Debug.Log("estamos dentro del for: "+x);
            switch (x)
            {
                case 1:
                    StartCoroutine(printh());
                    break;
                case 2:
                    StartCoroutine(printe());
                    break;
                case 3:
                    StartCoroutine(printhour());
                    break;
                case 4:
                    StartCoroutine(printsco());
                    break;
                default: break;
            }
       
    }
    private IEnumerator printh()
    {
        yield return new WaitForSeconds(1);
        rooms.SetActive(true);
        mori = GameObject.Find("monedascontroll").GetComponent<Monedas>();
        sh.GetComponent<TextMeshProUGUI>().text = mori.canthab.ToString();
        sh.SetActive(true);
    }

    private IEnumerator printe()
    {
        yield return new WaitForSeconds(2);
        enemy.SetActive(true);
        mori = GameObject.Find("monedascontroll").GetComponent<Monedas>();
        se.GetComponent<TextMeshProUGUI>().text = mori.enemigosVencidos.ToString();
        se.SetActive(true);
    }

    private IEnumerator printhour()
    {
        yield return new WaitForSeconds(3);
        time.SetActive(true);
        mori = GameObject.Find("monedascontroll").GetComponent<Monedas>();
        if (mori.hour < 10)
        {
            shour.GetComponent<TextMeshProUGUI>().text = "0" + mori.hour;
        }
        else
        {
            shour.GetComponent<TextMeshProUGUI>().text = mori.hour.ToString();
        }
        
        if (mori.min < 10)
        {
            smin.GetComponent<TextMeshProUGUI>().text = "0"+mori.min;
        }
        else
        {
            smin.GetComponent<TextMeshProUGUI>().text = mori.min.ToString();
        }

        float second = mori.sec;
        int changesec = (int)second;
        if (mori.sec < 10)
        {

            sseg.GetComponent<TextMeshProUGUI>().text = "0"+changesec;
        }
        else
        {
            sseg.GetComponent<TextMeshProUGUI>().text = changesec.ToString();
        }
        
        shour.SetActive(true);
        slash.SetActive(true);
        smin.SetActive(true);
        slash2.SetActive(true);
        sseg.SetActive(true);
    }

    private IEnumerator printsco()
    {
        yield return new WaitForSeconds(4);
        score.SetActive(true);
        mori = GameObject.Find("monedascontroll").GetComponent<Monedas>();
        sp.GetComponent<TextMeshProUGUI>().text = mori.puntaje.ToString();
        sp.SetActive(true);
    }

}
