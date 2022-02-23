using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class NodesSetting
{
    public Transform StartPoint;
    public Vector2 Paddings;
    public List<Node> Prefabs;
    public Node Empty;
    public NodeFX Fx;
}

[System.Serializable]
public class Deltas
{
    public float DestroyNodes;
    [Tooltip("how many cells higher should the node spawn what would then fall")]
    public int SpawnNode;
    public float Refill;
}
public class Board : MonoBehaviour
{
    [SerializeField] private Match3 _match3;
    [SerializeField] private NodesSetting _settings;
    [SerializeField] private Deltas _deltas;
    [SerializeField] private BuffHandler _buffHandler;
    [SerializeField] private AudioEffects _audioEffects;
   

    private Grid _grid;
    private Node[,] _nodes;
    public bool CanSwap { get; private set; }
    public Node[,] Nodes => _nodes;
    public Grid Grid => _grid;

    public UnityAction<int> ScoreIncreased;
    public UnityAction SwapNode;
    public UnityAction SwapEnd { get; set; }

    public UnityAction<List<MatchNode>> MatchDestoy;

    public void Init(Grid grid)
    {
        _grid = Instantiate(grid);
        _nodes = new Node[_grid.Size.x, _grid.Size.y];
        CanSwap = true;
        FillBoard();
    }

    private void FillBoard()
    {
        do
        {
            _nodes = new Node[_grid.Size.x, _grid.Size.y];
            for (int i = 0; i < _grid.Size.x; i++)
            {
                for (int j = 0; j < _grid.Size.y; j++)
                {
                    if (_grid[i, j].Type != NodeType.Empty)
                    {
                        var spawnedNode = SpawnRandomNode(i, j);
                        if (_grid[i, j].Modifier != null)
                        {
                            spawnedNode.SetModifier(_grid[i, j].Modifier);
                        }
                    }
                    else
                    {
                        SpawnEmptyNode(i, j);
                    }
                }
            }
        } while (_match3.TryFindTurns(_nodes) == false);
    }
    
    private IEnumerator ReFillBoard()
    {
        yield return Refill();

        while(_match3.TryFindTurns(_nodes) == false)
        {
            yield return RecreateBoard();
        }
        
    }

    private IEnumerator Refill()
    {
        for (int i = _grid.Size.x - 1; i >= 0; i--)
        {
            for (int j = _grid.Size.y - 1; j >= 0; j--)
            {
                if (_nodes[i, j] == null)
                {
                    ReFillNode(i, j);
                    yield return null;
                }
            }
        }
    }

    private IEnumerator RecreateBoard()
    {
        yield return Refill();

        foreach (var node in _nodes)
        {
            if (_grid[node.X, node.Y].Type == NodeType.Empty)
                continue;

            StartCoroutine(_settings.Fx.ZoomOut(node));
            yield return null;
        }


        for (int i = 0; i < _grid.Size.x; i++)
        {
            for (int j = 0; j < _grid.Size.y; j++)
            {
                if (_grid[i, j].Type == NodeType.Empty)                
                    continue;

                var node = GetRandomNode(i, j, _nodes, true);
                _nodes[i, j].ChangeColor(node);
                StartCoroutine(_settings.Fx.ZoomIn(_nodes[i, j]));
                yield return null;

            }
        }

    }

   
    private void ReFillNode(int i, int j)
    {
        if (_grid[i, j].Type == NodeType.Empty)        
            return;
        
        if (i == 0)
        {
            SpawnRefilledNode(i, j);
            return;
        }
            
        Node upperNode = null;
        int xUpperNode = -1;
        for (int x = i - 1; x >= 0; x--)
        {
            if (_nodes[x, j] == null)
            {
                continue;                      
            }
            else if (_grid[x, j].Type == NodeType.Empty)
            {
                continue;
            }
            upperNode = _nodes[x, j];
            xUpperNode = x;
            break;
        }

        if (upperNode == null)
        {
            _nodes[i, j] = SpawnRefilledNode(i, j);
            return;
        }

        else if (upperNode.IsMovable() == false)        
            return;            
       
        if (_grid[xUpperNode, j].Type == NodeType.Empty)        
            return;
        

        
        _settings.Fx.Move(upperNode, GetPosition(i, j));
        _nodes[i, j] = _nodes[xUpperNode, j];
        
        _nodes[i, j].Coordinates = new Vector2Int(i, j);

        _nodes[xUpperNode, j] = null;

    }
    

    private Node SpawnEmptyNode(int i, int j)
    {
        return SpawnNode(_settings.Empty, i, j);
    }
    private Node SpawnRandomNode(int i, int j, bool compareWithNearby = true)
    {
        return SpawnNode(GetRandomNode(i, j, compareWithNearby), i, j);
    }

    private Node SpawnRefilledNode(Node node, int i, int j)
    {
        _nodes[i, j].transform.localPosition = GetPosition(_deltas.SpawnNode, j);
        _settings.Fx.Move(_nodes[i, j], GetPosition(i, j));

        return _nodes[i, j];
    }

    private Node SpawnRefilledNode(int i, int j)
    {
        return SpawnRefilledNode(SpawnRandomNode(i, j, false), i, j);
    }
    private Node SpawnNode(Node node, int i, int j)
    {
        if (node == null)
        {
            return null;
        }
        _nodes[i, j] = Instantiate(node, transform);
        _nodes[i, j].transform.localPosition = GetPosition(i, j);
        _nodes[i, j].Coordinates = new Vector2Int(i, j);

        return _nodes[i, j];
    }

    private Vector3 GetPosition(int i, int j)
    {
        return new Vector3(j * _settings.Paddings.y, -i * _settings.Paddings.x, 0);
    }

    private Node GetRandomNode(int i, int j, Node[,] nodes, bool compareWithNearby = true)
    {
        List<Node> avaliableNodes = new List<Node>(_settings.Prefabs);
        if (compareWithNearby)
        {
            if (i > 0 && nodes[i - 1, j] != null)
                avaliableNodes.Remove(_settings.Prefabs.Find(x => x.Color == nodes[i - 1, j].Color));
            if (j > 0 && nodes[i, j - 1] != null)
                avaliableNodes.Remove(_settings.Prefabs.Find(x => x.Color == nodes[i, j - 1].Color));
        }

        int randomIndex = Random.Range(0, avaliableNodes.Count);
        var resutl = avaliableNodes[randomIndex];

        return resutl;
    }
    private Node GetRandomNode(int i, int j, bool compareWithNearby = true)
    {
        return GetRandomNode(i, j, _nodes, compareWithNearby);
    }

    
    private void TouchNearby(int i, int j)
    {
        List<Node> nodes = new List<Node>();
        if (i > 0) nodes.Add(_nodes[i - 1, j]);
        if (i < _grid.Size.x - 1) nodes.Add(_nodes[i + 1, j]);

        if (j > 0) nodes.Add(_nodes[i, j - 1]);
        if (j < _grid.Size.y - 1) nodes.Add(_nodes[i, j + 1]);

        foreach (var node in nodes)
        {            
            if (node != null)
                if (_grid[node.X, node.Y].Type != NodeType.Empty && _nodes[i, j] != null)
                        if(_nodes[i, j].Modifier == null)
                            node.NearbyTouch();                
        }
    }

    private List<Node> NodesTouchedByBuff(Node node)
    {
        if (node.Buff != null)
        {
            var NodesToTouch = node.Buff.NodesToTouch(node.X, node.Y, _nodes).ToList();
            var avaliableNodesToTouch = new List<Node>();
            foreach (var item in NodesToTouch)
            {
                if (item != null)
                {
                    if (_grid[item.X, item.Y].Type != NodeType.Empty)
                    {
                        avaliableNodesToTouch.Add(item);
                    }
                }
                
            }
            return avaliableNodesToTouch;
        }
        return null;
    }

    public void Swap(Node node1, Node node2)
    {
        node1.Swap(node2);
        Node tempNode = _nodes[node1.X, node1.Y];

        _nodes[node1.X, node1.Y] = _nodes[node2.X, node2.Y];
        _nodes[node2.X, node2.Y] = tempNode;
    }

    public bool IsNearby(Node node1, Node node2)
    {
        if (_grid[node1.X, node1.Y].Type == NodeType.Empty || _grid[node2.X, node2.Y].Type == NodeType.Empty)
        {
            return false;
        }
        Vector2Int coordinates = new Vector2Int(Mathf.Abs(node1.X - node2.X), Mathf.Abs(node1.Y - node2.Y));
        if (coordinates.x + coordinates.y == 1)
        {
            return true;
        }
        

        return false;
    }

    private List<Node> Unique(List<List<Node>> matches, List<Node> additional)
    {
        List<Node> result = new List<Node>(additional);
        foreach (var node in additional)
        {
            var check = matches.Find(x => x.Find(x => x == node));
            if (check != null)
            {
                result.Remove(node);
            }
        }
        return result;
    }

    public List<Node> Unique(List<MatchNode> parent, List<Node> additional)
    {
        List<Node> result = new List<Node>(additional);
        List<Node> tempParent = (from node in parent select node.Node).ToList();
        foreach (var node in additional)
        {
            if (tempParent.Contains(node))
            {
                result.Remove(node);
            }
        }
        return result;
    }


    private List<MatchNode> NodesToTouch(List<List<Node>> matches)
    {
        List<MatchNode> result = new List<MatchNode>();
        foreach (var match in matches)
        {
            if (match.Contains(null) || match.Find(x=> _grid[x.X, x.Y].Type == NodeType.Empty))            
                continue;            

            var buff = _buffHandler.GetBuffForMatch(match.Count);
            if (buff != null)            
                if (match[0].Buff != null)                
                    buff = null;
                
            
            for (int i = 0; i < match.Count; i++)
            {
                MatchNode tempNode = null;

                if (buff != null)
                {
                    if (i == match.Count - 1 )
                        tempNode = new MatchNode(match[i], buff);                    
                    else                    
                        tempNode = new MatchNode(match[i], isCreateBuff:true, match.Last());                    
                }
                else
                    tempNode = new MatchNode(match[i]);

                result.Add(tempNode);
            }


            foreach (var node in match)
            {
                var touchedByBuff = NodesTouchedByBuff(node);
                if (touchedByBuff != null)
                {
                    touchedByBuff.Add(node);
                    var nodesToAdd = Unique(matches, touchedByBuff);
                    nodesToAdd = Unique(result, nodesToAdd);
                    result.AddRange((from nodeToAdd in nodesToAdd select new MatchNode(nodeToAdd, true)).ToList());
                }
            }
        }

        return result;

    }
    private IEnumerator TouchNodes(List<Node> nodes)
    {
        yield return TouchNodes((from node in nodes select new MatchNode(node)).ToList());
    }

    private IEnumerator TouchNodes(List<MatchNode> nodes)
    {
        MatchDestoy?.Invoke(nodes);
        foreach (var matchNode in nodes)
        {
            if (matchNode.BuffToSet != null)
            {
                if (_nodes[matchNode.Node.X, matchNode.Node.Y])
                {
                    _nodes[matchNode.Node.X, matchNode.Node.Y].SetBuff(matchNode.BuffToSet);
                }
            }
            else
            {
                if (matchNode.TouchedByBuff == false)
                {
                    bool isNearbyTouch = true;
                    int index = nodes.IndexOf(matchNode);

                    if (index > 0)                    
                        if (nodes[index - 1].Node.Modifier != null)                        
                            isNearbyTouch = false;                        
                    
                    if (index < nodes.Count - 1)                    
                        if (nodes[index + 1].Node.Modifier != null)                        
                            isNearbyTouch = false;
                        
                    
                    if (isNearbyTouch)                    
                        TouchNearby(matchNode.Node.X, matchNode.Node.Y);
                    
                   
                }
                _nodes[matchNode.Node.X, matchNode.Node.Y] = matchNode.Node.Touch();
            }

            if (_nodes[matchNode.Node.X, matchNode.Node.Y] == null)
            {
                StartCoroutine(DestroyNode(matchNode));
            }
            yield return null;
        }
    }

    private IEnumerator DestroyNode(MatchNode matchNode)
    {
        if (matchNode.IsCreateBuff)
        {
            yield return DestroyNode(matchNode.Node, matchNode.NodeWithBuff.transform.localPosition);
        }
        else
        {
            yield return DestroyNode(matchNode.Node);
        }
    }

    private IEnumerator DestroyNode(Node node)
    {        
        yield return _settings.Fx.DestroyAnimation(node);
        if (node != null)
            Destroy(node.gameObject);
        node = null;
    }
    private IEnumerator DestroyNode(Node node, Vector3 destination)
    {
        if (node.Buff != null)
        {
            yield return DestroyNode(node);
            yield break;
        }
        yield return _settings.Fx.DestroyAnimation(node, destination);
        if (node != null)
            Destroy(node.gameObject);
        node = null;
    }

    public IEnumerator MatchHandler(List<Node> nodes, bool touchedByBuff = false)
    {
        yield return MatchHandler((from node in nodes select new MatchNode(node, touchedByBuff)).ToList());
    }

    public IEnumerator MatchHandler(List<MatchNode> nodes)
    {

        yield return TouchNodes(nodes);
        yield return new WaitForSeconds(_deltas.Refill);
        yield return ReFillBoard();
        yield return null;
    }
    public IEnumerator Match3Handler()
    {
        CanSwap = false;
        while (_match3.TryFindMatches(out var matches))
        {
            var nodes = NodesToTouch(matches);
            yield return MatchHandler(nodes);
        }
        CanSwap = true;
        SwapEnd?.Invoke();
        yield return null;
    }    

    public Node GetNodeByColor(NodeColor color)
    {
        var result = _settings.Prefabs.Find(x=> x.Color == color);
        if (result != null)
        {
            return result;
        }

        throw new System.Exception("Cant find prefab with color - " + color);
    }

    public IEnumerator TouchedByBuster(int i, int j, NodeBuff buff)
    {
        var list = buff.NodesToTouch(i, j, Nodes).ToList();
        list.Add(_nodes[i, j]);
        yield return MatchHandler(list, true);
        yield return ReFillBoard();
        yield return Match3Handler();
    }
}

