using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BusterView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Text _text;

    private int _count = 0;
    private NodeBuff _nodeBuff;

    public NodeBuff NodeBuff => _nodeBuff;

    public UnityAction<BusterView> BusterClicked;

    public void Init(NodeBuff nodeBuff, int count)
    {
        _nodeBuff = nodeBuff;
        _count = count;
        _image.sprite = nodeBuff.Sprite;
        _text.text = _count.ToString();
    }
    public void OnBusterClick()
    {
        if (_count <= 0)        
            return;
        
        BusterClicked?.Invoke(this);
    }

    public void FinnalyClick(int count)
    {
        _count = count;
        _text.text = _count.ToString();
    }

}
