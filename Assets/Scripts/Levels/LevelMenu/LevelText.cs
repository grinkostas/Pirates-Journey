using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelText : LevelMenuElement
{
    [SerializeField] private Text _text;
    [SerializeField] private string _mainText;

    protected override void OnOpened()
    {
        _text.text = _mainText + " " + _levelMenu.Level.Id;
    }
}
