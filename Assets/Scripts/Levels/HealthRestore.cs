using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRestore : MonoBehaviour
{
    [SerializeField] private float _minutesForOneHealth;
    [SerializeField] private int _maxHealth = 5;
    private float _secondsToRestore => _minutesForOneHealth * 60;

    private void Awake()
    {
        var objects = FindObjectsOfType<HealthRestore>();
        if (objects.Length > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        StartCoroutine(Load());
    }

    private IEnumerator Load()
    {
        float secondsWait = 5;
        while (SaveSystem.Loaded == false)
        {
            yield return new WaitForSeconds(secondsWait);
        }
        yield return Restore();
    }
    

    private IEnumerator Restore()
    {
        yield return RestoreWhileAfk();
        while (true)
        {
            if (SaveSystem.Data.health < _maxHealth)
            {
                SaveSystem.Data.secondsToNextHealth -= Time.unscaledDeltaTime;
                if (SaveSystem.Data.secondsToNextHealth <= 0)
                {
                    SaveSystem.Data.secondsToNextHealth = _secondsToRestore;
                    SaveSystem.Data.health += 1;
                    yield return SaveSystem.Save();
                }
            }
            yield return null;
        }        
    }

    private IEnumerator RestoreWhileAfk()
    {
        if (SaveSystem.Loaded == false)        
            yield break;
        
        if (SaveSystem.Data.health < _maxHealth)
        {            
            yield return SaveSystem.SecondsFromLastUpdate();
            float wastedSeconds = SaveSystem.SecondsBetweenLastUpdate;
            SkipSeconds(wastedSeconds);
            yield return SaveSystem.Save();
        }
        yield return null;       
        
    }

    private void SkipSeconds(float wastedSeconds)
    {
        int maxHealthToRestore = _maxHealth - SaveSystem.Data.health;
        float additionalSeconds = 0;
        int healthToRestore = 0;

        if (wastedSeconds > SaveSystem.Data.secondsToNextHealth)
        {
            healthToRestore++;
            wastedSeconds -= SaveSystem.Data.secondsToNextHealth;
            SaveSystem.Data.secondsToNextHealth = _secondsToRestore;
        }

        if (wastedSeconds > _secondsToRestore)
        {
            healthToRestore += (int)(wastedSeconds / _secondsToRestore);
            additionalSeconds = wastedSeconds - (healthToRestore * _secondsToRestore);
            if (healthToRestore > maxHealthToRestore)
            {
                healthToRestore = maxHealthToRestore;
            }
        }
        else
        {
            additionalSeconds = wastedSeconds;
        }

        SaveSystem.Data.health += healthToRestore;
        if (SaveSystem.Data.health >= _maxHealth)
        {
            SaveSystem.Data.secondsToNextHealth = _secondsToRestore;
        }
        else
        {
            SaveSystem.Data.secondsToNextHealth -= additionalSeconds;
        }
    }

    private void Pause()
    {
        StartCoroutine(SaveSystem.Save());
    }

    private void Unpause()
    {
        StartCoroutine(RestoreWhileAfk());
    }

    
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Pause();
        }
        else
        {
            Unpause();
        }
    }

    private void OnApplicationQuit()
    {
        Pause();
    }


}
