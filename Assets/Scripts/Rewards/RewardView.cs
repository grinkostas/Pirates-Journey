using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Text _text;

    public void Init(RewardData reward)
    {
        gameObject.SetActive(true);
        _image.sprite = reward.Reward.Sprite;
        _text.text = reward.Count.ToString();
    }
}
