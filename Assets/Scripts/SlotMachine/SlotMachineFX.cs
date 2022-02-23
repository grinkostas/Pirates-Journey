using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMachineFX : MonoBehaviour
{
    [SerializeField] private AnimationCurve _speedCurve;
    [SerializeField] private float _duration;

    public AnimationCurve SpeedCurve => _speedCurve;
    public float Duration => _duration;

}
