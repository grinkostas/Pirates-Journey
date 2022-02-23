using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CasinoRewardMenu : Menu
{
    [SerializeField] private Text _countText;
    [SerializeField] private Image _buffImage;
    public void Show(Sprite sprite, int count = 1)
    {
        base.Show();
        _buffImage.sprite = sprite;
        _countText.text = count.ToString() + "x";
        
    }
}
