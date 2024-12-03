using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMannagger : MonoBehaviour
{
    public static GameMannagger Instance;
    public List<Personajes> personajes;
    private void Awake()
    {
        if (GameMannagger.Instance == null)
        {
            GameMannagger.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
