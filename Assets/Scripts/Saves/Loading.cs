using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    [SerializeField] private Menu _loadingMenu;

    private void Start()
    {
        _loadingMenu.Show();
        StartCoroutine(Load());
    }

    private IEnumerator Load()
    {        
        yield return SaveSystem.Load();
        _loadingMenu.Hide();
    }
    
    public void OpenLevels()
    {
        SceneManager.LoadScene("Levels");
    }
}
