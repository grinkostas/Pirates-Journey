using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Reward : MonoBehaviour
{
    public abstract Sprite Sprite { get; }
   
    public abstract void Take(int count = 1);
}
