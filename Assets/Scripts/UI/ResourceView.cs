using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public abstract class ResourceView : MonoBehaviour
{
    [SerializeField] protected Text _text;

    public static UnityAction ValueChange;
    private void OnEnable()
    {
        ValueChange += Reload;
    }

    private void OnDisable()
    {
        ValueChange -= Reload;
    }

    protected virtual void Start()
    {
        Reload();
    }

    protected abstract void Reload();
}
