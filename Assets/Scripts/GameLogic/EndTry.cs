using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndTry : MonoBehaviour
{
    [SerializeField] private Board _board;
    [SerializeField] private Game _game;
    [SerializeField] private float _timeUntilShowMenu;
    
    [SerializeField] private VictoryMenu _victoryMenu;
    [SerializeField] private DefeatMenu _defeatMenu;
    [SerializeField] private PauseMenu _pauseMenu;
    [SerializeField] private ScoreView _scoreView;

    [SerializeField] private GoalBehaviour _goal;
    [SerializeField] private GameLimit _limit;

    
    private void OnEnable()
    {
        _game.LevelCompleted += OnLevelCompleted;

        _limit.EndLimit += OnEndLimit;
    }

    
    private void OnLevelCompleted()
    {
        OnEndLimit();
    }

    private void OnEndLimit()
    {
        if (_board.CanSwap)
        {
            EndGame();
        }
        else
        {
            _board.SwapEnd += EndGame;
        }
    }

    private void EndGame()
    {
        _limit.EndLimit -= OnEndLimit;
        _board.SwapEnd -= EndGame;

        StartCoroutine(ShowEndGameMenu());
    }

    private IEnumerator ShowEndGameMenu()
    {
        yield return new WaitForSeconds(_timeUntilShowMenu);


        if (_goal.AllGoalsReceived())
        {
            Victory();
        }
        else
        {
            _defeatMenu.Show();
        }
    }

    private void Victory()
    {
        _victoryMenu.Show();
        if (SaveSystem.Loaded)
        {
            var level = SaveSystem.Data.Levels.Find(x => x.LevelId == _game.Level.Id);
            if (level == null)
            {
                SaveSystem.CompleteLevel(_game.Level.Id, _victoryMenu.Stars);
            }
            foreach (var reward in _game.Level.Rewards)
            {
                reward.Reward.Take(reward.Count);
            }
        }
    }

    public void Pause()
    {
        _pauseMenu.Show();
    }
}
