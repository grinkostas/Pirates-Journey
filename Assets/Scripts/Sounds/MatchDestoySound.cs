using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchDestoySound : MonoBehaviour
{
    [SerializeField] private Board _board;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<MatchSound> _matchSoundes;

    [SerializeField] private AudioClip _defaultDestroySound;

    private void OnEnable()
    {
        _board.MatchDestoy += OnMatchDestoy;
    }

    private AudioClip FindClip(MatchType matchType)
    {
        AudioClip clip = _defaultDestroySound;
        var item = _matchSoundes.Find(x => x.Type == matchType);
        if (item != null)        
            clip = item.Sound;
        
        return clip;
    }

    private AudioClip GetClip(List<MatchNode> nodes)
    {

        var buff = nodes.Find(x => x.BuffToSet != null);
        if (buff != null)
            return FindClip(MatchType.CreateBuster);

        return FindClip(MatchType.Default);


    }
    private void OnMatchDestoy(List<MatchNode> nodes)
    {
        _audioSource.clip = GetClip(nodes);
        _audioSource.Play();
    }
}

[System.Serializable]
public class MatchSound
{
    public MatchType Type;
    public AudioClip Sound;
}


public enum MatchType
{
    Default, 
    CreateBuster, 
    ReceiveGoal
}
