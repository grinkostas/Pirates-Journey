using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMenu : Menu
{
    [SerializeField] private Shop _shop;
    [SerializeField] private List<ShopItem> _shopItems;
    [SerializeField] private Menu _purchased;
    [SerializeField] private ConfirmMenu _confirmMenu;

    public override void Hide()
    {
        Saver.Update?.Invoke();
        base.Hide();
    }

    private void Start()
    {
        for (int i = 0; i < _shopItems.Count; i++)
        {
            if (_shop.Tradable.Count > i)
            {
                _shopItems[i].Init(_shop.Tradable[i], _confirmMenu);
                _shopItems[i].Buyed += OnItemBuyed;
            }
            else
            {
                break;
            }
        }
    }

    private void OnItemBuyed()
    {
        _purchased.Hide();
        _purchased.Show();
    }

    private void OnDisable()
    {
        for (int i = 0; i < _shopItems.Count; i++)
        {
            _shopItems[i].Buyed -= OnItemBuyed;
        }
    }

}
