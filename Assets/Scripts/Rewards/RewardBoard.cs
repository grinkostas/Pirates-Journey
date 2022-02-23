using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardBoard : MonoBehaviour
{
    [SerializeField] private List<RewardView> _rewardViews;

    public void Init(List<RewardData> rewards)
    {        
        for (int i = 0; i < _rewardViews.Count; i++)
        {
            if(rewards.Count > i)
                _rewardViews[i].Init(rewards[i]);
        }
    }
}
