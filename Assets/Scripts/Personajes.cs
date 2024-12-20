using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NuevoPersonaje", menuName = "Personaje")]
public class Personajes : ScriptableObject
{
    public GameObject personajeJugable;

    public Sprite imagen;

    public string nombre;

    public int vidaMaxima, armaduraMaxima,tempvida,temparma, velocidad;

}
