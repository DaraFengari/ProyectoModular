using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMannager : MonoBehaviour
{
    public static GameMannager Instance;
    public List<Remedios> remedios;

    private void Awake()
    {
        if (GameMannager.Instance == null)
        {
            GameMannager.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
