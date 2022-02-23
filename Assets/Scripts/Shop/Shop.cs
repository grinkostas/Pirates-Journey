using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tradable
{
    public IShopItem Item;
    public int Price;
}

[CreateAssetMenu(menuName = "Shop/New Shop")]
public class Shop : ScriptableObject
{
    public List<Tradable> Tradable;
}
