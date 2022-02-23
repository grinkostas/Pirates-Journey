using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelMenu : Menu
{
    [SerializeField] private Level _currentLevel;

    private Level _level;
    public Level Level => _level;
    public UnityAction Opened;

    public void OpenLevel()
    {
        if (SaveSystem.Data.health > 0)
        {
            _currentLevel.Init(_level);
            SceneManager.LoadScene("Game");
        }        

    }

    public void Show(Level level)
    {
        base.Show();
        _level = level;
        Opened?.Invoke();
    }


}
