using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Request
{
    private string _url = "http://google.com";

    private string _result;
    private bool _isConnected;
    public string Result => _result;
    public bool Connection => _isConnected;

    public Request(){}
    public Request(string url)
    {
        _url = url;
    }
    public IEnumerator CheckInternetConnection()
    {
        UnityWebRequest request = UnityWebRequest.Get(_url);
        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
        {
            _isConnected = false;
        }
        else
        {
            _isConnected = true;
        }

    }
    private IEnumerator Create(UnityWebRequest request)
    {
        yield return CheckInternetConnection();
        
        if (_isConnected == false)
        {
            yield break;
        }

        
        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            _result = request.downloadHandler.text;
        }
        

    }

    public IEnumerator CreateGet(string parametrs)
    {
        UnityWebRequest request = UnityWebRequest.Get(_url + parametrs);
        yield return Create(request);
    }

    public IEnumerator CreatePost(WWWForm form)
    {
        UnityWebRequest request = UnityWebRequest.Post(_url, form);
        yield return Create(request);
    }
}
