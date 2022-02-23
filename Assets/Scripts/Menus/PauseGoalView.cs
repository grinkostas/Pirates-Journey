using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGoalView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Text _text;
    public void Init(Sprite sprite, Goal goal)
    {

        _image.sprite = sprite;
        _text.text = goal.CompletedCount.ToString() + "/" + goal.RequiredCount.ToString();
    }
}
