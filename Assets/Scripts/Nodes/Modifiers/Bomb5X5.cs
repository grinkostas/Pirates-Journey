using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb5X5 : NodeBuff
{
   
    public override IEnumerable<Node> NodesToTouch(int i, int j, Node[,] board)
    {
        for (int x = i - 2; x < i + 3; x++)
        {
            for (int y = j - 2; y < j + 3; y++)
            {
                if (x == i && y == j)
                    continue;

                if (Check(x, y, board))
                    yield return board[x, y];

            }
        }
    }

}
