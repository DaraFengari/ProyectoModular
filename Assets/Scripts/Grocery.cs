using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nuevatienda", menuName = "Tienda")]
public class Grocery : ScriptableObject
{
    public Sprite imagen;
    public string nombre;
    public int precio;
    public string descripcion;

}
