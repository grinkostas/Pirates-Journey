using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class ScoreSetting
{
    public int FirstStar;
    public int SecondStar;
    public int ThirdStar;

    public int[] Stars => new int[]{FirstStar, SecondStar, ThirdStar };

    public int GetStarCount(int score)
    {
        int starCount = 0;
        foreach (var item in Stars)
        {
            if (score >= item)
            {
                starCount++;
            }
        }
        return starCount;
    }
}

public class ScoreView : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private int _scorePerNode;
    [SerializeField] private Slider _slider;
    
    [SerializeField] private ScoreSetting _scoreSetting;
    [SerializeField] private ScoreStar[] _scoreBar;

    private int _score = 0;

    public int MaxScore => _scoreSetting.ThirdStar;
    public int CurrentStarCount => _scoreSetting.GetStarCount(_score);

    #region
    private void OnEnable()
    {
        Node.Destoy += OnNodeDestoy;
        Node.ModifierDestoy += OnModifierDestoy;
    }

    private void OnDisable()
    {
        Node.Destoy -= OnNodeDestoy;
        Node.ModifierDestoy -= OnModifierDestoy;
    }
    private void Start()
    {
        UpdateScore();
    }

    private void OnNodeDestoy(NodeColor color)
    {
        _score += _scorePerNode;
        UpdateScore();
    }

    private void OnModifierDestoy(NodeModifier modifier)
    {
        _score += modifier.Score;
        UpdateScore();
    }
    #endregion

    private void UpdateScore()
    {
        _scoreText.text = _score.ToString();
        _slider.value = (float)_score / _scoreSetting.ThirdStar;
    }
}
