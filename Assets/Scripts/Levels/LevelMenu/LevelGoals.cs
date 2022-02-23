using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGoals : LevelMenuElement
{
    [SerializeField] private List<GoalView> _goalViews;
    protected override void OnOpened()
    {
        var goals = _levelMenu.Level.Goals;
        for (int i = 0; i < goals.Count; i++)
        {
            _goalViews[i].Init(goals[i]);
        }
    }
}
