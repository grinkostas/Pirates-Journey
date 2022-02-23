using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GoldView : ResourceView
{
    protected override void Reload()
    {
        _text.text = SaveSystem.Data.Gold;
    }
}
