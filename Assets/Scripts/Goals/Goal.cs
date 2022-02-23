using System;
using UnityEngine.Events;

[Serializable]
public class Goal
{
    public GoalItem GoalItem;
    public int RequiredCount;
    public bool Received { get; private set; }
    public int CompletedCount { get; private set; }

    public int Count => RequiredCount - CompletedCount;

    public UnityAction Touched;
    public UnityAction Receive;
    public void Touch()
    {
        if (Received == true) return;
        
        CompletedCount += 1;
        if (RequiredCount - CompletedCount == 0)
        {
            Received = true;
            Receive?.Invoke();
        }

        Touched?.Invoke();
    }
    public void Reset()
    {
        Received = false;
        CompletedCount = 0;

    }
}
