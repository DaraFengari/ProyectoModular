using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nuevoremedio", menuName = "Remedio")]
public class Remedios : ScriptableObject
{
    public Sprite imagen;
    public string nombre;
    public int precio;
    public string descripcion;

}
