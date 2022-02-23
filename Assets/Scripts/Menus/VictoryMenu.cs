using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryMenu : Menu
{
    [SerializeField] private Stars _activeStars;
    [SerializeField] private ScoreView _scoreView;
    [SerializeField] private RewardBoard _rewardBoard;
    [SerializeField] private Level _currentLevel;
    public int Stars => _scoreView.CurrentStarCount;

    public override void Show()
    {
        base.Show();
        _rewardBoard.Init(_currentLevel.Rewards);
        _activeStars.Show(_scoreView.CurrentStarCount);
    }


}
