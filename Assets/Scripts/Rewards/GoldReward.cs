using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldReward : Reward
{
    [SerializeField] private Sprite _sprite;       
    public override Sprite Sprite => _sprite;

    public override void Take(int count = 100)
    {
        SaveSystem.Data.gold += count;
    }
}
