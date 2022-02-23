using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingView : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private string _standartText;
    [SerializeField] private int _maxDotsCount;
    [SerializeField] private int _minDotsCount;
    [SerializeField] private float _delay;

    private int _currentDotsCount = int.MaxValue;

    private void OnEnable()
    {
        StartCoroutine(Loading());   
    }

    private void OnDisable()
    {
        StopCoroutine(Loading());
    }
    private IEnumerator Loading()
    {
        while (gameObject.activeSelf)
        {
            yield return NextDot();
        }
    }

    private IEnumerator NextDot()
    {
        yield return new WaitForSeconds(_delay);
        if (_currentDotsCount > _maxDotsCount)
        {
            _currentDotsCount = _minDotsCount;
        }
        string result = _standartText + new string('.', _currentDotsCount);
        _currentDotsCount++;
    }

    
}
