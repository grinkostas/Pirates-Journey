using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public static class SaveSystem
{
    private const string UserIdPref = "UserId";


    private static int _userId = 0;

    public static SaveData Data;

    public static DateTime CurrentTime;

    public static float SecondsBetweenLastUpdate;

    public static bool Loaded {  get { return _userId != 0;  } }    

    private static IEnumerator GetUserId()
    {
        _userId = PlayerPrefs.GetInt(UserIdPref, 0);
        if (_userId == 0)
        {
            yield return GetTime();
            WWWForm form = new WWWForm();
            string query = "insert into Users (Name, LastUpdateTime) VALUES(\'new user', \'" + CurrentTime.ToString() + "\')";
            form.AddField("Insert", query);

            Request request = new Request(DataBase.Url);
            yield return request.CreatePost(form);

            _userId = int.Parse(request.Result);
            Debug.Log(_userId);
            PlayerPrefs.SetInt(UserIdPref, _userId);
        }

    }

    private static IEnumerator GetUserData()
    {
        Request request = new Request(DataBase.Url);
        yield return request.CreateGet("?id=" + _userId.ToString());
        Data = JsonUtility.FromJson<SaveData>(request.Result);
    }

    public static IEnumerator Save()
    {
        yield return GetTime();
        Data.LastUpdateTime = CurrentTime.ToString();
        Request request = new Request(DataBase.Url);
        WWWForm form = new WWWForm();
        form.AddField("Update", 1);
        form.AddField("Json", JsonUtility.ToJson(Data));
        yield return request.CreatePost(form);
    }

    public static IEnumerator SecondsFromLastUpdate()
    {
        yield return GetTime();
        SecondsBetweenLastUpdate = (float)(CurrentTime - Convert.ToDateTime(Data.LastUpdateTime)).TotalSeconds;
        yield return null;
    }
    

    private static IEnumerator GetTime()
    {        
        Request requestTime = new Request(DataBase.Url + "?time=1");
        yield return requestTime.CreateGet("");
        string time = requestTime.Result;
        CurrentTime = Convert.ToDateTime(time);
    }


    public static IEnumerator Load()
    {
        yield return GetUserId();
        yield return GetUserData();
    }

    private static void ChangeBusterCount(string busterId, int delta)
    {
        var buster = Data.Busters.Find(x=> x.BusterId == busterId);
        if (buster == null)
        {
            buster = new Buster(_userId.ToString(), busterId, delta.ToString());
            Data.Busters.Add(buster);
        }
        else
        {
            int count = buster.count + delta;
            if (count < 0)
            {
                count = 0;   
            }
            buster.Count = count.ToString();
        }
    }


    public static void CompleteLevel(string levelId, int starCount)
    {
        var level = Data.Levels.Find(x=>x.LevelId == levelId);
        if (level == null)
        {
            Data.Levels.Add(new Lvl(_userId.ToString(), levelId, starCount.ToString()));
        }
        else
        {
            if (int.Parse(level.StarCount) < starCount)
            {
                level.StarCount = starCount.ToString();
            }
        }
    }

    public static void AddBusters(string busterId, int count = 1)
    {
        ChangeBusterCount(busterId, count);
    }

    public static void RemoveBusters(string busterId, int count = 1)
    {
        ChangeBusterCount(busterId, count * -1);
    }

}
