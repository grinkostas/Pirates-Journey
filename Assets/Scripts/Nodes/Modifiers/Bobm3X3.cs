using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bobm3X3 : NodeBuff
{
    public override IEnumerable<Node> NodesToTouch(int i, int j, Node[,] board)
    {
        for (int x = i - 1; x < i + 2; x++)
        {
            for (int y = j - 1; y < j + 2; y++)
            {
               
                if (Check(x, y, board))                
                    yield return board[x, y];
                
            }
        }
    }
}
