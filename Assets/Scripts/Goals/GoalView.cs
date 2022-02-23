using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Text _text;
    [SerializeField] private Image _receiveImage;

    private Goal _goal;
    public Goal Goal => _goal;
    public Sprite Sprite => _image.sprite;
    private void UpdateUI()
    {
        if (_goal.Received == true)
        {
            Receive();
        }
        if (_text != null)
        {
            _text.text = _goal.Count.ToString();
        }
    }

    private void Receive()
    {
        _goal.Touched -= UpdateUI;
        _goal.Receive -= Receive;
        if (_text != null)
        {
            _text.gameObject.SetActive(false);
        }
        if (_image != null)
        {
            _receiveImage.gameObject.SetActive(true);
        }

    }    

    public void Init(Goal goal)
    {
        gameObject.SetActive(true);
        _goal = goal;
        _goal.Reset();
        _receiveImage.gameObject.SetActive(false);
        _text.gameObject.SetActive(true);
        _image.sprite = _goal.GoalItem.Sprite;
        _goal.Touched += UpdateUI;
        _goal.Receive += Receive;
        UpdateUI();

    }
   
   
}
