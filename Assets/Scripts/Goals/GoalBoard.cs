using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GoalBoard : MonoBehaviour
{
    [SerializeField] private List<GoalView> _goalViews;
    public List<GoalView> GoalViews => _goalViews;

    public void Init(GoalBehaviour goals)
    {
        for (int i = 0; i < goals.Goals.Count; i++)
        {
            _goalViews[i].Init(goals.Goals[i]);
        }
    }

    public GoalView Find(Node node)
    {
        try
        {
            return _goalViews.Find(x=>x.Goal.GoalItem.Equals(node));
        }
        catch(System.Exception)
        {
            return null;
        }        
    }    


}
