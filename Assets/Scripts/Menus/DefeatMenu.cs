using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefeatMenu : Menu
{
    [SerializeField] private Button _button;

    public override void Show()
    {
        base.Show();
        if (SaveSystem.Loaded)
        {        
            if (SaveSystem.Data.health <= 0)
            {
                _button.interactable = false;
            }
        }
    }

   
}
