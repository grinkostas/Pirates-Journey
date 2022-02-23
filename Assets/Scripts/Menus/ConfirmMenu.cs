using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmMenu : Menu
{
    [SerializeField] private Button _button;

    private System.Action _function;
    private void OnEnable()
    {
        _button.onClick.AddListener(Click);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Click);
    }
    public void Show(System.Action function)
    {
        base.Show();
        _function = function;
    }

    private void Click()
    {
        if (_function != null)
        {
            _function();
        }
        base.Hide();
    }
}
