using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealhDonationItem : DonationShopItem
{
    public override void Buy()
    {
        SaveSystem.Data.health += _count;
    } 
}
