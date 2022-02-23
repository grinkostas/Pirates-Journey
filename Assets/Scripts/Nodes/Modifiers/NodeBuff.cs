using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public abstract class NodeBuff : IShopItem
{
    [SerializeField] private string _id;
    [SerializeField] private bool _canBeDestoroyedByTheSame;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private AudioSource _audioSource;

    public float EffectsDuration
    {
        get
        {
            float[] durations = new float[]
            {
                _audioSource.clip.length,
                _particleSystem.main.duration
            };
            return durations.Max();
        }
    }
    public override Sprite Sprite => _spriteRenderer.sprite;
    public string Id => _id;
    public bool CanBeDestroyedByTheSame => _canBeDestoroyedByTheSame;

    public abstract IEnumerable<Node> NodesToTouch(int i, int j, Node[,] node);

    

    public static List<Node> Unique(List<Node> nodesParent, List<Node> nodesAdditional)
    {
        List<Node> result = new List<Node>();
        foreach (var item in nodesAdditional)
        {
            if (nodesParent.Contains(item) == false)
            {
                result.Add(item);
            }
        }
        return result;
    }

    public bool Check(int x, int y, Node[,] board)
    {
        if (x < 0 || y < 0) 
            return false;

        if (x > board.GetLength(0) - 1 || y > board.GetLength(1) - 1)
            return false;

        if (board[x, y] != null)        {
            if (board[x, y].Color != NodeColor.Empty)            
                return true;
        }
        return false;
    }
    
    public override void Buy()
    {
        SaveSystem.AddBusters(Id);
    }

    public void Activate()
    {
        _particleSystem.Play();
        _audioSource.Play();
    }
}
