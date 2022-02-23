using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
internal class HealthRelativelySprite
{
    public int Health;
    public Sprite Sprite;
}

public class NodeModifier : GoalItem
{
    [SerializeField] private int _destroyScore;
    [SerializeField] private int _hitToDestroy;
    [SerializeField] private bool _isMovable;
    [SerializeField] private bool _canBeTouchedByNearbies;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private List<HealthRelativelySprite> _healthRelativelySprite;

    [SerializeField] private ParticleSystem _particalSystem;
    private int _health;
    private Sprite _startSprite = null;

    public int MaxHealth => _hitToDestroy;
    public override Sprite Sprite { 
        get 
        {
            Sprite sprite;
            if (_startSprite != null)
            {
                sprite = _startSprite;
            }
            else
            {
                sprite = _spriteRenderer.sprite;
            }
            return sprite; 
        } 
    }
    public override Type Type => Type.NodeModifier;
    public bool IsMovable => _isMovable;
    public bool CanBeTouchedByNearbies => _canBeTouchedByNearbies;
    public int Score => _destroyScore;

    public UnityAction Remove;
    
    private void OnEnable()
    {
        _health = _hitToDestroy;
        _startSprite = _spriteRenderer.sprite;
    }
    public void Touch()
    {
        _health -= 1;
        UpdateSprite();
        if (_audioSource != null)
        {
            _audioSource.Play();
        }
        if (_particalSystem != null)
        {
            _particalSystem.Play();
        }
        if (_health == 0)
        {
            _spriteRenderer.enabled = false;          
            
            Remove?.Invoke();
        }
    }

    private void UpdateSprite()
    {
        if (_healthRelativelySprite.Count <= 0 || _health == 0)
        {
            return;
        }
        Sprite sprite = _healthRelativelySprite.Find(x=> x.Health == _health).Sprite;
        if (sprite != null)
        {
            _spriteRenderer.sprite = sprite;
        }
        
    }

    public static bool Equals(NodeModifier modifier1, NodeModifier modifier2)
    {
        if (modifier1 == null || modifier2 == null)
        {
            return false;
        }
        if (modifier1.MaxHealth == modifier2.MaxHealth && modifier1.Sprite == modifier2.Sprite)
        {
            return true;
        }
        return false;
    }
}
