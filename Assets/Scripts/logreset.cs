using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class logreset : MonoBehaviour
{
    private logreset loge;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        if (loge == null)
        {
            loge = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("userId", 0);
        PlayerPrefs.Save();
        int dogo= PlayerPrefs.GetInt("userId");
        Debug.Log(dogo);
    }
}
