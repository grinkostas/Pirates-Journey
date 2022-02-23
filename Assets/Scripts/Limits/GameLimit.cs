using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameLimit : MonoBehaviour
{
    [SerializeField] private TimeLimit _timeLimit;
    [SerializeField] private TurnsLimit _turnsLimit;
    [SerializeField] private Game _game;

    private Limit _currentLimit;
    private Level Level => _game.Level;

    public Limit CurrentLimit => _currentLimit;
    public UnityAction EndLimit;
    public void Init()
    {
        if (Level.LimitType == LimitType.Time)
        {
            _currentLimit = _timeLimit;
        }
        else
        {
            _currentLimit = _turnsLimit;
        }
        _currentLimit.Init(Level.LimitValue);
        _currentLimit.EndLimit += EndLimit;
    }

    public void StartLimit()
    {
        _currentLimit.StartLimit();
    }


}
