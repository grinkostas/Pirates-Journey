using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class Slot : MonoBehaviour
{
    [SerializeField] private Transform _nextSlotPosition;

    private SpriteRenderer _spriteRenderer;
    private NodeBuff _nodeBuff;

    public Vector3 NextSlotPosition => _nextSlotPosition.position;
    public NodeBuff NodeBuff => _nodeBuff;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init(NodeBuff nodeBuff)
    {
        _spriteRenderer.sprite = nodeBuff.Sprite;
        _nodeBuff = nodeBuff;
    }
}
