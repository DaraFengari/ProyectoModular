using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class list : MonoBehaviour
{
    public static list Instance;
    public List<Tiendas> tiendas;

    private void Awake()
    {
        if (list.Instance == null)
        {
            list.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
