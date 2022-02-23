using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SlotMachineColumn : MonoBehaviour
{
    [SerializeField] private Transform _startPostion;
    [SerializeField] private Transform _slotsParent;
    [Space]
    [SerializeField] private int _visibleSlotsCount;
    [SerializeField] private int _resultIndex;
    [SerializeField] private int _slotsOffset;
    [SerializeField] private int _slotsCount;
    [Space]
    [SerializeField] private Slot _slotPrefab;
    [SerializeField] private List<NodeBuff> _prizes;
    [SerializeField] private SlotMachineDestroyer _destroyer;

    private List<Slot> _spawnedSlots = new List<Slot>();

    public UnityAction<Slot> SpinEnded;

    private void OnEnable()
    {
        _destroyer.SlotCollided += OnSlotCollided;
    }

    private void OnDisable()
    {
        _destroyer.SlotCollided -= OnSlotCollided;
    }
    private void OnSlotCollided(Slot slot)
    {
        if (_spawnedSlots.Contains(slot) == false)
        {
            return;
        }
        _spawnedSlots.Remove(slot);
        SpawnRandomSlot();
    }

    private void SpawnStartSlots()
    {
        int iStart = _spawnedSlots.Count;
        for (int i = iStart; i < _visibleSlotsCount + _slotsOffset; i++)
        {
            SpawnRandomSlot();
        }
    }

    private void SpawnRandomSlot()
    {
        SpawnSlot(GetRandomSlot());
    }

    private void SpawnSlot(NodeBuff prize)
    {
        var slot = Instantiate(_slotPrefab, GetSlotPosition(), Quaternion.identity, _slotsParent);
        slot.Init(prize);
        _spawnedSlots.Add(slot);
    }

    private NodeBuff GetRandomSlot()
    {
        var prizes = new List<NodeBuff>(_prizes);
        if (_spawnedSlots.Count > 0)
        {
            prizes.Remove(_spawnedSlots[_spawnedSlots.Count - 1].NodeBuff);
        }
        int randIndex = Random.Range(0, prizes.Count);
        return prizes[randIndex];
    }
    private Vector3 GetSlotPosition()
    {
        Vector3 result = _startPostion.position;
        if (_spawnedSlots.Count > 0)
        {
            result = _spawnedSlots[_spawnedSlots.Count - 1].NextSlotPosition;
        }
        return result;
    }

    private IEnumerator Spin(AnimationCurve curve, float duration)
    {
        Vector3 startPosition = _slotsParent.position;
        Vector3 delta = Vector3.up * _slotPrefab.transform.localScale.y * (_slotsCount - _slotsOffset - _visibleSlotsCount); 
        float wastedTime = 0;
        float progress = 0;
        while (progress <= 1)
        {
            progress = wastedTime / duration;
            wastedTime += Time.deltaTime;
            _slotsParent.position = startPosition -  delta * curve.Evaluate(progress);
            yield return null;
        }
        SpinEnded?.Invoke(_spawnedSlots[_resultIndex]);
    }

    public void Init()
    {
        SpawnStartSlots();
    }

    public void StartSpin(AnimationCurve curve, float duration)
    {
        StartCoroutine(Spin(curve, duration));
    }

    

}
