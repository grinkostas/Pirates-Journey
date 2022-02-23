using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelMenuElement : MonoBehaviour
{
    [SerializeField] protected LevelMenu _levelMenu;

    private void OnEnable()
    {
        _levelMenu.Opened += OnOpened;
    }

    private void OnDisable()
    {
        _levelMenu.Opened -= OnOpened;
    }

    protected abstract void OnOpened();
}
