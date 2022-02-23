using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthView : ResourceView
{
    [SerializeField] private Text _timeText;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(UpdateTime());
    }
    protected override void Reload()
    {
        _text.text = SaveSystem.Data.Health;
    }

    private IEnumerator UpdateTime()
    {

        while (true)
        {
            System.TimeSpan time = System.TimeSpan.FromSeconds(SaveSystem.Data.secondsToNextHealth);
            _timeText.text = time.ToString(@"mm\:ss");
            yield return null;
        }
    }

}
