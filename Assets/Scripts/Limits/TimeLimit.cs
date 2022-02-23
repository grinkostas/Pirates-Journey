using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TimeLimit : Limit
{
    [SerializeField] private Slider _slider;

    private float _currentTime;

    private void Start()
    {
        _slider.value = 0;
    }
    public override void StartLimit()
    {
        _currentTime = LimitValue;
        StartCoroutine(TimeDescreaser());
    }

    private IEnumerator TimeDescreaser()
    {
        float wastedTime = 0;
        while (_currentTime > 0)
        {
            wastedTime += Time.deltaTime;
            _currentTime -= Time.deltaTime;
            _slider.value = wastedTime / LimitValue;
            yield return null;
        }
        EndLimit?.Invoke();
        yield return null;
    }
}
