using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Limit : MonoBehaviour
{
    protected float LimitValue;
    public UnityAction EndLimit;
    
    public void Init(float value) 
    {
        LimitValue = value;
        gameObject.SetActive(true);
    }

    public abstract void StartLimit();
}
