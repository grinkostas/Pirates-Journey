using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelReward : LevelMenuElement
{
    [SerializeField] private RewardBoard _rewardBoard;
    protected override void OnOpened()
    {
        _rewardBoard.Init(_levelMenu.Level.Rewards);
    }
}
