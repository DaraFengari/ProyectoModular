using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManaager : MonoBehaviour
{
    public static GameManaager Instance;
    public List<Personajes> personajes;

    private void Awake()
    {
        if (GameManaager.Instance == null)
        {
            GameManaager.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
