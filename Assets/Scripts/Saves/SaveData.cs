using System;
using System.Collections;
using System.Collections.Generic;


[Serializable]
public class Lvl
{
    public string id;
    public string UserId;
    public string LevelId;
    public string StarCount;

    public int starCount {
        get 
        {
            return int.Parse(StarCount);
        }
        set
        {
            StarCount = value.ToString();
        }
    }
    
    public Lvl(string userId, string levelId, string starCount)
    {
        UserId = userId;
        LevelId = levelId;
        StarCount = starCount;
    }
}

[Serializable]
public class Buster
{
    public string id;
    public string UserId;
    public string BusterId;
    public string Count;

    public int count
    {
        get
        {
            return int.Parse(Count);
        }
        set
        {
            Count = value.ToString();
        }
    }

    public Buster(string userId, string busterId, string count)
    {
        UserId = userId;
        BusterId = busterId;
        Count = count;
    }
}

[Serializable]
public class SaveData
{
    public string id;
    public string Name;
    public string Gold;
    public string Health;
    public string LastUpdateTime;
    public string SecondsToNextHealth;

    public List<Lvl> Levels;
    public List<Buster> Busters;



    public int gold
    {
        get
        {
            return int.Parse(Gold);
        }
        set
        {            
            Gold = value.ToString();
            ResourceView.ValueChange?.Invoke();
        }
    }

    public int health
    {
        get
        {
            return int.Parse(Health);
        }
        set
        {            
            Health = value.ToString();
            ResourceView.ValueChange?.Invoke();
        }
    }

    public float secondsToNextHealth
    {
        get
        {
            return float.Parse(SecondsToNextHealth);
        }
        set
        {
            if (value < 0)
            {
                value = 0;
            }
            SecondsToNextHealth = value.ToString();
        }
    }
}



