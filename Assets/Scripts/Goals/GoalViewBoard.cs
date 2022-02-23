using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalViewBoard : MonoBehaviour
{
    [SerializeField] private GoalBoard _goalBoard;
    [SerializeField] private List<PauseGoalView> _pauseGoalViews;

    private void OnEnable()
    {
        for (int i = 0; i < _goalBoard.GoalViews.Count; i++)
        {
            if (_goalBoard.GoalViews[i].Goal != null)
            {
                _pauseGoalViews[i].Init(_goalBoard.GoalViews[i].Sprite, _goalBoard.GoalViews[i].Goal);
                _pauseGoalViews[i].gameObject.SetActive(true);
            }

        }
    }
}
