using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum NodeType
{
    Default,
    Empty
}

public class GridNode : MonoBehaviour
{
    [SerializeField] private NodeType _type;
    [SerializeField] private NodeModifier _modifier = null;
    
    [Header("Type")]
    [SerializeField] private Image _typeImage;
    [SerializeField] private Color _defaultColor;
    [Header("Modifier")]
    [SerializeField] private Image _modifierImage;
    [SerializeField] private Color _emptyColor;
    [SerializeField] private Color _filledColor;
    public NodeType Type => _type;
    public NodeModifier Modifier => _modifier;

    private void OnValidate()
    {
        if (_type == NodeType.Empty)
        {
            Color color = _defaultColor;
            color.a = 0;
            _typeImage.color = color;
        }
        else
            _typeImage.color = _defaultColor;

        if (_modifier != null)
        {
            _modifierImage.sprite = _modifier.Sprite;
            _modifierImage.color = _filledColor;
        }
        else
            _modifierImage.color = _emptyColor;
        
        
    }
}

