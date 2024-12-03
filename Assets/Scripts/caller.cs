using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caller : MonoBehaviour
{
    private ScoreManager punts;
    private Monedas moni;
    private void OnEnable()
    {
        punts = GameObject.Find("Scripter").GetComponent<ScoreManager>();
        moni = GameObject.Find("monedascontroll").GetComponent<Monedas>();
        Debug.Log("tranquilizate");
        punts.OnGameOver(moni.puntaje, moni.canthab, (int)moni.hour, (int)moni.min, (int)moni.sec, moni.enemigosVencidos);
    }
}
