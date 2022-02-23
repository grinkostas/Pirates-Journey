using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IBuyable
{
    public void Buy();
}

public abstract class IShopItem : MonoBehaviour, IBuyable
{
    public abstract Sprite Sprite { get; }
    public abstract void Buy();
}
