using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Text _text;

    public UnityAction Buyed;
    private Tradable _tradable;
    private ConfirmMenu _confirmMenu;

    public void Init(Tradable tradable, ConfirmMenu confirmMenu)
    {
        gameObject.SetActive(true);
        _confirmMenu = confirmMenu;
        _tradable = tradable;
        _image.sprite = _tradable.Item.Sprite;
        _text.text = _tradable.Price.ToString();
    }

    public void Buy()
    {
        _confirmMenu.Show(RealBuy);        
    }

    private void RealBuy()
    {
        if (SaveSystem.Data.gold >= _tradable.Price)
        {
            SaveSystem.Data.gold -= _tradable.Price;
            _tradable.Item.Buy();
            Buyed?.Invoke();
        }
    }


    
}
