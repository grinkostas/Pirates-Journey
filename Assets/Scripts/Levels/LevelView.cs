using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelView : MonoBehaviour
{
    [SerializeField] private CompletedLevel _completedLevel;
    [SerializeField] private IncompletedLevel _incompletedLevel;

    private LevelMenu _levelMenu;
    private Level _level;
    public void Init(Level level, LevelMenu levelMenu)
    {
        _level = level;
        _levelMenu = levelMenu;
        var completedLevels = SaveSystem.Data.Levels;
        var levelData = completedLevels.Find(x=> x.LevelId == _level.Id);
        if (levelData != null)
        {
            _completedLevel.Show(levelData.starCount, _level.Id);
        }
        else
        {
            _incompletedLevel.Show(_level.Id);
        }
    }

    public void ButtonClick()
    {
        if (_level.RequiredLevelToEnter != null)
        {
            var level = SaveSystem.Data.Levels.Find(x=> (x.LevelId == _level.RequiredLevelToEnter.Id) && x.starCount > 0);
            if (level == null)
            {
                return;
            }
        }
        _levelMenu.Show(_level);
    }
}
