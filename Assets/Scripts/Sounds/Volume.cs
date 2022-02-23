using UnityEngine;
using UnityEngine.Audio;

public class Volume : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private string _exposedParametr;

    private const float _soundOffVolume = -80;
    protected const float _minVolume = -40;

    public float volume {
        get
        {
            float volume;
            _mixer.GetFloat(_exposedParametr, out volume);
            return volume;
        }
        protected set
        {
            _mixer.SetFloat(_exposedParametr, value);
            PlayerPrefs.SetFloat(_exposedParametr, value);
        }
    }

    private void Start()
    {
        _mixer.SetFloat(_exposedParametr, PlayerPrefs.GetFloat(_exposedParametr));
    }

    public void SoundOff()
    {
        volume = _soundOffVolume;
    }


}

