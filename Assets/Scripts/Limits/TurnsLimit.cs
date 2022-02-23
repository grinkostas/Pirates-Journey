using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnsLimit : Limit
{
    [SerializeField] private Text _turnesText;
    [SerializeField] private Board _board;    

    private float _currentTurns;

    public void OnEnable()
    {
        _turnesText.text = LimitValue.ToString();
    }

    public override void StartLimit()
    {
        _currentTurns = LimitValue;
        OnSwapNode();
        _board.SwapNode += OnSwapNode;
    }

    private void OnSwapNode()
    {
        _currentTurns--;
        _turnesText.text = _currentTurns.ToString();
        if (_currentTurns <= 0)
        {
            EndLimit?.Invoke();
            _board.SwapNode -= OnSwapNode;
        }
    }

}
