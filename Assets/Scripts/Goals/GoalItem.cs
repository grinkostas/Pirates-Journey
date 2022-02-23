using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum Type
{
    Node,
    NodeModifier
}

public abstract class GoalItem : MonoBehaviour
{
    public static UnityAction<GoalItem> Touched;
    public abstract Sprite Sprite { get; }
    public abstract Type Type { get; }

    public bool Equals(GoalItem item)
    {
        if (item.Sprite == Sprite && item.Type == Type)
        {
            return true;
        }

        return false;
    }

    private void OnDestroy()
    {
        Touched?.Invoke(this);
    }
}


