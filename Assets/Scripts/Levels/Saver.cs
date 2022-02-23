using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Saver : MonoBehaviour
{
    public static UnityAction Update;

    private void OnEnable()
    {
        Update += Save;
    }
    private void OnDisable()
    {
        Update -= Save;
    }

    private void Save()
    {
        StartCoroutine(SaveSystem.Save());
    }

}
