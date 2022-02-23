using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMachineMenu : Menu
{
    [SerializeField] private List<SlotMachineColumn> _columns;
    [SerializeField] private SlotMachineFX _fx;
    [SerializeField] private float _delayStartColumns;
    [SerializeField] private int _spinPrice;
    [Space]
    [SerializeField] private CasinoRewardMenu _rewardMenu;
    [SerializeField] private Sprite _coinSprite;

    private int _finishedColumns = 0;
    private bool _isSpining = false;
    private List<Slot> _results = new List<Slot>();

    private void OnEnable()
    {
        Init();
        foreach (var item in _columns)
        {
            item.SpinEnded += OnSpinEnded;
        }
    }

    private void OnSpinEnded(Slot slot)
    {
        _finishedColumns++;
        _results.Add(slot);
        if (_finishedColumns == _columns.Count)
        {
            _isSpining = false;
            _finishedColumns = 0;
            GetPrize();
            _results.Clear();            
        }
        
        
    }

    private void GetPrize()
    {
        Sprite sprite = _coinSprite;
        int count = 0;
        bool show = false;
        Debug.Log(_results[0].NodeBuff.Id + " " + _results[1].NodeBuff.Id + " " + _results[2].NodeBuff.Id);
        if (_results[0].NodeBuff.Id == _results[1].NodeBuff.Id && _results[1].NodeBuff.Id == _results[2].NodeBuff.Id)
        {
            count = 2;
            SaveSystem.AddBusters(_results[0].NodeBuff.Id, count);
            sprite = _results[0].NodeBuff.Sprite;
            show = true;
        }
        else if (_results[0].NodeBuff.Id == _results[1].NodeBuff.Id || _results[1].NodeBuff.Id == _results[2].NodeBuff.Id)
        {
            show = true;
            float rand = (float)Random.Range(40, 60) / 10;
            count = (int)(_spinPrice * rand);
            SaveSystem.Data.gold += count;
        }
        if (show)
        {
            Saver.Update?.Invoke();
            _rewardMenu.Show(sprite, count);
        }
    }

    private void Init()
    {
        foreach (var item in _columns)
        {
            item.Init();
        }
    }

    private IEnumerator StartSpining()
    {
        _isSpining = true;
        foreach (var item in _columns)
        {
            item.StartSpin(_fx.SpeedCurve, _fx.Duration);
            yield return new WaitForSeconds(_delayStartColumns);
        }
    }
    public void Spin()
    {
        if (_isSpining == false)
        {
            int balance = SaveSystem.Data.gold;
            if (balance >= _spinPrice)
            {
                SaveSystem.Data.gold -= _spinPrice;
                StartCoroutine(StartSpining());
            }
        }
    }

    


}
