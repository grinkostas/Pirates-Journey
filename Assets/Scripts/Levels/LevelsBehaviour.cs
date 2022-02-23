using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsBehaviour : MonoBehaviour
{
    [SerializeField] private List<Level> _levels;
    [SerializeField] private List<LevelView> _levelViews;
    [SerializeField] private LevelMenu _levelMenu;

    private void Start()
    {
        int count = _levels.Count > _levelViews.Count ? _levelViews.Count : _levels.Count;
        for (int i = 0; i < count; i++)
        {
            _levelViews[i].Init(_levels[i], _levelMenu);
        }
    }
}
