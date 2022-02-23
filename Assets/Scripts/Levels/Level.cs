using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class RewardData
{
    public Reward Reward;
    public int Count;
}

[System.Serializable]
public enum LimitType
{
    Turns, 
    Time
}



[CreateAssetMenu(menuName ="Level/New Level")]
public class Level : ScriptableObject
{
    public string Id;
    public int StarCount;
    public bool IsCompeleted;
    public Level RequiredLevelToEnter;
    public Grid Grid;
    public List<Goal> Goals;
    public List<RewardData> Rewards;

    public LimitType LimitType;
    public float LimitValue;

   public void CompleteLevel(int startCount)
   {
        IsCompeleted = true;
        StarCount = startCount;
   }

    public void Init(Level level)
    {
        Id = level.Id;
        StarCount = level.StarCount;
        IsCompeleted = level.IsCompeleted;
        RequiredLevelToEnter = level.RequiredLevelToEnter;
        Grid = level.Grid;
        Goals = level.Goals;
        Rewards = level.Rewards;
        LimitType = level.LimitType;
        LimitValue = level.LimitValue;
    }



}
