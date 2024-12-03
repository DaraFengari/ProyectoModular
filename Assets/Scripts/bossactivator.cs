using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossactivator : MonoBehaviour
{
        public GameObject ravioli;

        private void Start()
        {
            ravioli.SetActive(false);
        }

    private void OnDestroy()
    {
        ravioli.SetActive(true);
    }
}
