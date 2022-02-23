using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundVolumeChanger : Volume
{
    [SerializeField] private Slider _slider;

    private void OnEnable()
    {
        _slider.value = (-volume + _minVolume) / _minVolume;      
    }

    public void OnSliderChange()
    {
        if (_slider.value == 0)
        {
            SoundOff();
            return;
        }
        float soundVolume = _minVolume - (_minVolume * _slider.value);
        volume = soundVolume;
    }
}
