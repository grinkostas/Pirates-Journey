using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeBackground : MonoBehaviour
{
    [SerializeField] private Color _selectedColor;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private Color _defaultColor;

    private void OnEnable()
    {
        _defaultColor = _spriteRenderer.color;
    }
    public void Select()
    {
        _spriteRenderer.color = _selectedColor;
    }
    public void Unselect()
    {
        _spriteRenderer.color = _defaultColor;
    }


}
