using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class Node : GoalItem
{
    [SerializeField] private NodeColor _color;
    [SerializeField] private NodeBackground _background;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private AudioSource _destroySound;

    public static UnityAction<NodeColor> Destoy;
    public static UnityAction<NodeModifier> ModifierDestoy;

    public Vector2Int Coordinates;
    public AudioSource DestroySound => _destroySound;
    public NodeModifier Modifier { get; private set; }
    public NodeBuff Buff { get; private set; }
    public NodeColor Color => _color;
    public int X => Coordinates.x;
    public int Y => Coordinates.y;
    public NodeBackground Background => _background;

    public SpriteRenderer SpriteRenderer => _spriteRenderer;
    public override Sprite Sprite => _spriteRenderer.sprite;
    public override Type Type => Type.Node;


    public void SetBuff(NodeBuff buff)
    {
        if (Buff != null)
        {
            return;
        }
        if (buff != null)
        {
            Buff = Instantiate(buff, transform);
        }
    }
    public void SetModifier(NodeModifier modifier)
    {
        if (Modifier != null || modifier == null)
        {
            return;
        }
        Modifier = Instantiate(modifier, transform);
        Modifier.Remove += OnModifierRemove;
    }

    public void ChangeColor(Node node)
    {
        ChangeColor(node.Color, node.Sprite);
    }

    public void ChangeColor(NodeColor color, Sprite sprite)
    {
        _color = color;
        _spriteRenderer.sprite = sprite;
    }

    private void OnModifierRemove()
    {
        Modifier.Remove -= OnModifierRemove;
        ModifierDestoy?.Invoke(Modifier);
        Modifier = null;
    }
    public Node Touch()
    {
        
        if (Modifier != null)
        {
            Modifier.Touch();
            return this;
        }
        Destoy?.Invoke(_color);
        return null;
    }

    public void NearbyTouch()
    {
        if (Modifier != null)
        {
            if (Modifier.CanBeTouchedByNearbies)
            {
                Modifier.Touch();
            }
        }
    }
    public void Select()
    {
        _background.Select();   
    }
    public void UnSelect()
    {
        _background.Unselect();
    }

    public void Swap(Node node)
    {
        Vector2Int temp = new Vector2Int(X, Y);
        Coordinates = node.Coordinates;
        node.Coordinates = temp;
    }

    public bool IsMovable()
    {
        if (_color == NodeColor.Empty)
        {
            return false;
        }

        if (Modifier != null)
        {
            if (Modifier.IsMovable == false)
            {
                return false;
            }
        }

        return true;
    }


}

public enum NodeColor
{
    Purple, 
    Yellow, 
    Pink, 
    Green, 
    Empty,
    Null,
    None
}
