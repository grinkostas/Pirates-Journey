using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowAndColumnBomb : NodeBuff
{
    public override IEnumerable<Node> NodesToTouch(int i, int j, Node[,] board)
    {
        for (int x = 0; x < board.GetLength(0); x++)
        {
            if (x == i)
                continue;

            if (Check(x, j, board))
                yield return board[x, j];
        }

        for (int y = 0; y < board.GetLength(1); y++)
        {
            if (y == j)
                continue;

            if (Check(i, y, board))
                yield return board[i, y];
        }
    }
}
