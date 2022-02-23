using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusterReward : Reward
{
    [SerializeField] private NodeBuff _nodeBuff;
    public override Sprite Sprite => _nodeBuff.Sprite;

    public override void Take(int count = 1)
    {
        SaveSystem.AddBusters(_nodeBuff.Id, count);
    }
}
