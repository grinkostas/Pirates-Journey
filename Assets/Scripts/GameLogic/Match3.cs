using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hint
{
    public Vector2Int FirstNode;
    public Vector2Int SecondNode;

    public Hint(int i1, int j1, int i2, int j2)
    {
        FirstNode = new Vector2Int(i1, j1);
        SecondNode = new Vector2Int(i2, j2);
    }

    public override string ToString()
    {
        return $"{FirstNode}, {SecondNode}";
    }
}

public class MatchNode
{
    public Node Node;
    public NodeBuff BuffToSet = null;

    public bool IsCreateBuff = false;
    public Node NodeWithBuff;

    public bool TouchedByBuff;
    public int MatchId = 0;

    public MatchNode(Node node)
    {
        Node = node;
    }
    public MatchNode(Node node, bool touchedByBuff) : this(node)
    {
        TouchedByBuff = touchedByBuff;
    }
    public MatchNode(Node node, NodeBuff buff) : this(node)
    {
        BuffToSet = buff;
    }

    public MatchNode(Node node, bool isCreateBuff, Node nodeWithBuff) : this(node)
    {
        IsCreateBuff = isCreateBuff;
        NodeWithBuff = nodeWithBuff;
    }

}

public class Match3 : MonoBehaviour
{
    [SerializeField] private Board _board;

    private List<List<Node>> _matches;

    private Vector2Int _size => new Vector2Int(_board.Nodes.GetLength(0), _board.Nodes.GetLength(1));

    private List<List<Node>> FindMatches(Node[,] nodes, bool returnFirst = false, bool onlyRows = false, bool onlyColumns = false)
    {
        var matches = new List<List<Node>>();
        int size = Mathf.Max(_size.x, _size.y);
        for (int i = 0; i < size; i++)
        {
            
            var tempMatches = new List<List<Node>>();
            if (i < _size.x && onlyColumns == false)
            {
                tempMatches.AddRange(FindMatchesInDirection(nodes, new Vector2Int(i, 0), new Vector2Int(0, 1), _size.y));
            }
            if (i < _size.y && onlyRows == false)
            {
                tempMatches.AddRange(FindMatchesInDirection(nodes, new Vector2Int(0, i), new Vector2Int(1, 0), _size.x));
            }
            if (tempMatches.Count > 0)
            {                
                matches.AddRange(tempMatches);                
            }

        }
        var finalList = matches.Distinct().ToList();
        return finalList;
    }

    private Hint CheckSwap(Node[,] nodes, int i, int j, Vector2Int swapDelta)
    {
        if (i < _size.x - swapDelta.x && j < _size.y - swapDelta.y)
        {
            bool onlyRows = false, onlyColumns = false;
            Node[,] changedBoard = new Node[_size.x, _size.y];
            System.Array.Copy(nodes, changedBoard, _size.x * _size.y);
            Node tempNode = changedBoard[i, j];
            changedBoard[i, j] = changedBoard[i + swapDelta.x, j + swapDelta.y];
            changedBoard[i + swapDelta.x, j + swapDelta.y] = tempNode;
            if (swapDelta.x == 0)
                onlyColumns = true;

            if (swapDelta.y == 0)
                onlyRows = true;

            var matches = FindMatches(changedBoard, returnFirst: true, onlyRows: onlyRows, onlyColumns: onlyColumns);
            if (matches.Count > 0)
                return new Hint(i, j, i + 1, j);
        }
        return null;
    }


    public Hint FindHint(Node[,] nodes)
    {
        Vector2Int[] swapsDelta = new Vector2Int[] { new Vector2Int(1, 0), new Vector2Int(0, 1) };
        for (int i = 0; i < _size.x; i++)
        {
            for (int j = 0; j < _size.y; j++)
            {
                foreach(var swapDelta in swapsDelta)
                {
                    Hint hint = CheckSwap(nodes, i, j, swapDelta);
                    if (hint != null)
                    {
                        return hint;
                    }
                }
            }
        }
        return null;
    }

    public bool TryFindTurns(Node[,] nodes)
    {
        return FindHint(nodes) != null;
    }
 
    public bool TryFindMatches(out List<List<Node>> matches)
    {
        matches = FindMatches(_board.Nodes);
        return matches.Count > 0;
    }

    private List<List<Node>> FindMatchesInDirection(Node[,] nodes, Vector2Int startIndex, Vector2Int directionDelta, int size)
    {
        
        List<List<Node>> matches = new List<List<Node>>();
        List<Node> tempNodes = new List<Node>();
        tempNodes.Add(nodes[startIndex.x, startIndex.y]);
        Vector2Int currentIndex = new Vector2Int(startIndex.x, startIndex.y);
        for (int j = 0; j < size - 1; j++)
        {
            Node current = nodes[currentIndex.x, currentIndex.y];
            currentIndex.x += directionDelta.x;
            currentIndex.y += directionDelta.y;
            Node next = nodes[currentIndex.x, currentIndex.y];
            var match = Match(current, next, ref tempNodes);
            if (match != null)
            {
                matches.Add(match);
            }
        }
        if (tempNodes.Count >= 3)
        {
            matches.Add(tempNodes);
        }
        return matches;
    }


    private List<Node> Match(Node current, Node next, ref List<Node> tempNodes)
    {
        if (current == null || next == null)
        {
            return null;
        }
        if (next.Color == current.Color && next.Color != NodeColor.Empty) 
        {
            tempNodes.Add(next);
        }
        else
        {
            List<Node> temp = new List<Node>(tempNodes);
            tempNodes.Clear();
            tempNodes.Add(next);
            if (temp.Count >= 3)
            {
                return temp;
            }
        }

        return null;

       
    }
}
