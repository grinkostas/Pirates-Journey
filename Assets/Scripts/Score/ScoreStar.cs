using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Slider))]
public class ScoreStar : MonoBehaviour
{
    [SerializeField] private int _scoreToStar;
    [SerializeField] private ScoreView _score;

    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void Start()
    {
        _slider.value = (float)_scoreToStar / _score.MaxScore;
    }
}
