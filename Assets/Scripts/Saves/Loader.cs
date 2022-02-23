using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    private void Awake()
    {
        if (SaveSystem.Loaded == false)
        {
            SceneManager.LoadScene("Loading");
        }
    }
}
