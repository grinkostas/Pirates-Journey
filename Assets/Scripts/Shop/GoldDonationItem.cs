using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldDonationItem : DonationShopItem
{
    public override void Buy()
    {
        SaveSystem.Data.gold += _count;
        SaveSystem.Save();
    }


   
}
