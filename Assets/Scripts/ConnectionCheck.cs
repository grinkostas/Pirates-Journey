using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectionCheck : MonoBehaviour
{
    [SerializeField] private Menu _noConnectionMenu;
    [SerializeField] private float _checkDelay = 5;
    private IEnumerator CheckInternetConnection()
    {
        Request request = new Request();
        while (true)
        {
            yield return request.CheckInternetConnection();
            if (request.Connection == false)
            {
                _noConnectionMenu.Show();
            }
            else
            {
                _noConnectionMenu.Hide();
            }
            yield return new WaitForSeconds(_checkDelay);
        }
        
    }
    private void Start()
    {
        StartCoroutine(CheckInternetConnection());
    }
}
