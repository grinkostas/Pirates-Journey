using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public enum PaymentMethod
{
    Money,
    Gold,
    Ad
}

public abstract class DonationShopItem : MonoBehaviour, IBuyable
{
    [SerializeField] protected string _itemId;
    [SerializeField] protected PaymentMethod _paymentMethod;
    [SerializeField] protected string _valute;
    [SerializeField] protected string _price;
    [SerializeField] protected Text _priceText;
    [SerializeField] protected int _count;
    [SerializeField] protected Text _countText;
    [SerializeField] protected Button _buyButton;

    public abstract void Buy();
    public virtual void Init()
    {
        if (_paymentMethod == PaymentMethod.Money)
        {
            _buyButton.interactable = false;
        }
        _priceText.text = _price + " " + _valute;
        _countText.text = _count.ToString();
    }
    private void OnEnable()
    {
        _buyButton.onClick.AddListener(OnBuyButtonClick);
        Init();
    }
    private void OnDisable()
    {
        _buyButton.onClick.AddListener(OnBuyButtonClick);
    }

    private void OnBuyButtonClick()
    {
        if (_paymentMethod == PaymentMethod.Gold)
        {
            BuyForGold();
        }
    }

    private void BuyForGold()
    {
        if (SaveSystem.Loaded)
        {
            if (SaveSystem.Data.gold >= int.Parse(_price))
            {
                SaveSystem.Data.gold -= int.Parse(_price);
                Buy();
            }
        }
    }


    
    
    
}
