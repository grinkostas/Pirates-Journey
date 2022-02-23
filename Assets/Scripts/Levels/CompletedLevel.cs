using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompletedLevel : Menu
{
    [SerializeField] private Star[] _stars;
    [SerializeField] private Text _text;
    
    public void Show(int stars = 1, string text = "0")
    {
        base.Show();
        for (int i = 0; i < stars; i++)
        {
            _stars[i].Show();
        }
        _text.text = text;
    }
}
