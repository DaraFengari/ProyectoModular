using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagger : MonoBehaviour
{
    public static GameManagger Instance;
    public List<Cakes> pasteles;

    private void Awake()
    {
        if (GameManagger.Instance == null)
        {
            GameManagger.Instance= this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
