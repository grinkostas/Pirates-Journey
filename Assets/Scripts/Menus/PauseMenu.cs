using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : Menu
{
    [SerializeField] private Game _game;
    [SerializeField] private ConfirmMenu _confirmMenu;

    private float _previousTimeScale = 1;

    private void Exit()
    {
        _game.OpenLevels();
    }

    public void Click()
    {
        _confirmMenu.Show(Exit);
    }
    public override void Show()
    {
        
        base.Show();
        
        _previousTimeScale = Time.timeScale;
        Time.timeScale = 0;
    }

    public override void Hide()
    {
        Time.timeScale = _previousTimeScale;
        base.Hide();
    }

    private void OnDisable()
    {
        Time.timeScale = _previousTimeScale;
    }
}
