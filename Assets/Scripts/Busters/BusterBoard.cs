using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BusterData
{
    public NodeBuff Buff;
    public BusterView View;
    public int Count;

    public BusterData(NodeBuff buff, BusterView view, int count)
    {
        Buff = buff;
        View = view;
        Count = count;
    }
}

public class BusterBoard : MonoBehaviour
{
    [SerializeField] private BusterView _busterViewPrefab;
    [SerializeField] private Busters _busters;
    [SerializeField] private Control _control;
    [SerializeField] private Image _overlay;

    private List<BusterView> _busterViews = new List<BusterView>();

    private void Start()
    {
        LoadBusters();
        _control.BusterClicked += OnBusterFinnalyClicked;
    }

    private void LoadBusters()
    {
        if (SaveSystem.Loaded == false)
            return;

        foreach (var buster in SaveSystem.Data.Busters)
        {
            var buff = _busters[buster.BusterId];
            if (buff == null && buster.count <= 0)
                continue;

            BusterView busterView = Instantiate(_busterViewPrefab, transform);
            busterView.Init(buff, buster.count);
            busterView.BusterClicked += OnBusterClicked;
            _busterViews.Add(busterView);

        }
    }

    private void OnDisable()
    {
        foreach(var item in _busterViews)
        {
            item.BusterClicked -= OnBusterClicked;
        }
        _control.BusterClicked -= OnBusterFinnalyClicked;
    }
    private void OnBusterClicked(BusterView view)
    {
        if (_overlay.gameObject.activeSelf)        
            _overlay.gameObject.SetActive(false);        
        else        
           _overlay.gameObject.SetActive(true);        

        _control.BusterClick(view.NodeBuff);
    }

    private void OnBusterFinnalyClicked(NodeBuff nodeBuff)
    {
        _overlay.gameObject.SetActive(false);
        var view = _busterViews.Find(x => x.NodeBuff.Id == nodeBuff.Id);

        var buster = SaveSystem.Data.Busters.Find(x => x.BusterId == nodeBuff.Id);
        buster.count -= 1;

        if (buster.count == 0)
        {
            _busterViews.Remove(view);
            Destroy(view.gameObject);
            return;
        }
        view.FinnalyClick(buster.count);
    }


}
