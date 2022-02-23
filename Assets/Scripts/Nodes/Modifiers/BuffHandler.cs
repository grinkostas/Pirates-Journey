using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
internal class BuffSetting
{
    public NodeBuff buff;
    public int matchInRow;
}


public class BuffHandler : MonoBehaviour
{
    [SerializeField] private List<BuffSetting> _buffs;

    public NodeBuff GetBuffForMatch(int matchInRow)
    {
        var buffs = _buffs.FindAll(x=> x.matchInRow == matchInRow);
        if (buffs != null)
        {
            if (buffs.Count > 0)
            {
                int index = 0;            
                index = Random.Range(0, buffs.Count);
                return buffs[index].buff;
            }
        }
        return null;

    }

}
