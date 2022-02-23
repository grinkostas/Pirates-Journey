using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalBehaviour : MonoBehaviour
{
    [SerializeField] private GoalBoard _goalBoard;
    private List<Goal> _goals;
    public List<Goal> Goals => _goals;
 
    private void OnEnable()
    {
        GoalItem.Touched += OnGoalTouched;
    }

    private void OnDisable()
    {
        GoalItem.Touched -= OnGoalTouched;
    }


    private void OnGoalTouched(GoalItem goalItem)
    {
        var goals = _goals.FindAll(x => x.GoalItem.Equals(goalItem));
        foreach (var goal in goals)
        {
            if (goal == null)            
                continue;                
            
            goal.Touch();
        }
        
    }
    public void Init(List<Goal> goals)
    {
        _goals = new List<Goal>(goals);
        foreach (var item in _goals)
        {
            item.Reset();
        }
        _goalBoard.Init(this);
    }
    public bool AllGoalsReceived()
    {
        foreach (var item in _goals)
        {
            if (item.Received == false)
            {
                return false;
            }
        }

        return true;

    }

}
