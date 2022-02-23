using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


public class Game : MonoBehaviour
{
    [SerializeField] private Level _level;
    [SerializeField] private Board _board;
    [SerializeField] private GoalBehaviour _goal;
    [SerializeField] private ScoreView _score;
    [SerializeField] private PauseMenu _pauseMenu;

    private const int MAX_STAR_COUNT = 3;
    public Level Level => _level;
    public UnityAction LevelCompleted;

    private void OnEnable()
    {
        _board.SwapEnd += OnSwapEnd;
    }

    private void Awake()
    {
        _board.Init(_level.Grid);
        _goal.Init(_level.Goals);
    }

    private void OnSwapEnd()
    {
        if (_goal.AllGoalsReceived() && _score.CurrentStarCount == MAX_STAR_COUNT)
        {
            LevelCompleted?.Invoke();
        }
    }

    public void OpenLevels()
    {
        SceneManager.LoadScene("Levels");
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            _pauseMenu.Show();
        }

    }
}


