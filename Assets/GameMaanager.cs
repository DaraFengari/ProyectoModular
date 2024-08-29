using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaanager : MonoBehaviour
{
    public static GameMaanager Instance;
    public List<Grocery> grocery;
    private void Awake()
    {
        if (GameMaanager.Instance == null)
        {
            GameMaanager.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
