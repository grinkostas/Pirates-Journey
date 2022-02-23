using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    [SerializeField] private List<Star> _stars;

    public void Show(int starCount)
    {
        for (int i = 0; i < starCount; i++)
        {
            _stars[i].gameObject.SetActive(true);
        }
    }
}
