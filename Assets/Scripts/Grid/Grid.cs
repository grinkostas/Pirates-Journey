using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private Vector2Int _size;

    private GridNode[,] _gridNodes;
    public Vector2Int Size => _size;

    private void Awake()
    {
        GetGridNode();
    }
   
    private void GetGridNode()
    {
        _gridNodes = new GridNode[_size.x, _size.y];
        GridNode[] gridNodes = GetComponentsInChildren<GridNode>();
        for (int i = 0; i < _size.x; i++)
        {
            for (int j = 0; j < _size.y; j++)
            {
                _gridNodes[i, j] = gridNodes[i * _size.y + j];
            }
        }
    }

    public GridNode this[int index, int index2]
    {
        get
        {
            return _gridNodes[index, index2];
        }
    }

}
