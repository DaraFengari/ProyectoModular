using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManaageer : MonoBehaviour
{
    public static GameManaageer Instance;
    public List<Personajes> personajes;
    private void Awake()
    {
        if(GameManaageer.Instance == null)
        {
            GameManaageer.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
