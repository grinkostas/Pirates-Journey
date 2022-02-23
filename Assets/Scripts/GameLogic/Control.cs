using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Control : MonoBehaviour
{
    [SerializeField] private Board _board;
    [SerializeField] private Match3 _match3;
    [SerializeField] private NodeFX _fx;
    [SerializeField] private GameLimit _limit;

    private Node _selectedNode = null;

    private bool _busterActive = false;
    private NodeBuff _nodeBuff;

    private Vector2 _swipeStart;
    private const float Min_Swipe_Distance = 0.1f;
    private Node _clickedNode;

    public UnityAction<NodeBuff> BusterClicked;

    private void Awake()
    {
        _selectedNode = null;
    }


    private void Start()
    {
        _limit.Init();
        _board.SwapEnd += OnSwapEnd;
    }

    private void OnSwapEnd()
    {
        StartGame();
        _board.SwapEnd -= OnSwapEnd;
    }

    private void StartGame()
    {
        _limit.StartLimit();
        if (SaveSystem.Loaded)
        {
            int health = SaveSystem.Data.health - 1;
            SaveSystem.Data.health = health;
        }
    }


    private void Update()
    {
        Swipe();
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            Click();
        }
        
    }

    private void Click()
    {        
        if (Input.GetMouseButtonDown(0))
        {
            Touch(Input.mousePosition);
        }
    }
   
    private void Touch(Vector2 position)
    {
        RaycastHit2D ray = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(position));
        if (ray)
        {
            if (ray.collider.TryGetComponent(out Node node))
            {
                ClickOnNode(node);
            }

        }
    }

    private void Swipe()
    {
        if (Input.touches.Length > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                _swipeStart = new Vector2(touch.position.x / Screen.width, touch.position.y / Screen.height);
                Touch(touch.position);
            }
            if (touch.phase == TouchPhase.Ended)
            {
                Vector2 swipeEnd = new Vector2(touch.position.x / Screen.width, touch.position.y / Screen.height);
                Vector2 swipe = swipeEnd - _swipeStart;
                if (swipe.magnitude > Min_Swipe_Distance)
                {
                    if (Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y))
                    {
                        if (swipe.x > 0)
                        {
                            ClickOnNode(_board.Nodes[_clickedNode.X , _clickedNode.Y + 1]);
                        }
                        else
                        {
                            ClickOnNode(_board.Nodes[_clickedNode.X , _clickedNode.Y - 1]);
                        }
                    }
                    else
                    {
                        if (swipe.y > 0)
                        {
                            ClickOnNode(_board.Nodes[_clickedNode.X - 1, _clickedNode.Y ]);
                        }
                        else
                        {
                            ClickOnNode(_board.Nodes[_clickedNode.X + 1, _clickedNode.Y ]);
                        }
                    }
                }
                _selectedNode = null;
            }
        }

    }

    private void ClickOnNode(Node node)
    {
        _clickedNode = node;
        if (_busterActive == false)
        {
            OnNodeClick(node);
        }
        else
        {
            if (_nodeBuff != null)
            {
                OnBusterClick(node);
            }

        }
    }

    private void OnNodeClick(Node node)
    {
        if (node.IsMovable() == false || _board.CanSwap == false)
        {
            return;
        }

        if (_selectedNode != null )
        {
            if (_selectedNode != node && _board.IsNearby(_selectedNode, node))
            {
                StartCoroutine(TrySwap(node));
            }
            
            _selectedNode = null;
            
        }
        else
        {
            _selectedNode = node;
        }
       
    }

    private void OnBusterClick(Node node)
    {
        if (_board.Grid[node.X, node.Y].Type == NodeType.Empty || _board.CanSwap == false)
        {
            return;
        }
        StartCoroutine(_board.TouchedByBuster(node.X, node.Y, _nodeBuff));
        BusterClicked?.Invoke(_nodeBuff);
        DisableBuster();
    }
    private IEnumerator TrySwap(Node node1)
    {
        Node node2 = _selectedNode;
        
        yield return Swap(node1, node2);
        if (_match3.TryFindMatches(out var matches))
        {
            _board.SwapNode?.Invoke();
            StartCoroutine(_board.Match3Handler());
        }
        else if(node1.Buff != null)
        {
            StartCoroutine(_board.TouchedByBuster(node2.X, node2.Y, node1.Buff));
        }
        else if(node2.Buff != null)
        {
            StartCoroutine(_board.TouchedByBuster(node1.X, node1.Y, node2.Buff));
        }
        else
        {
            yield return Swap(node1, node2);
        }

        
    }

    private IEnumerator Swap(Node node1, Node node2)
    {
        Vector3 tempPostion = node1.transform.localPosition;

        _board.Swap(node1, node2);
        StartCoroutine(_fx.MoveAnimation(node1, node2.transform.localPosition ));
        yield return _fx.MoveAnimation(node2, tempPostion);

    }

    private void EnableBuster(NodeBuff buff)
    {
        _busterActive = true;
        _nodeBuff = buff;
    }

    private void DisableBuster()
    {
        _busterActive = false;
        _nodeBuff = null;
    }

    public void BusterClick(NodeBuff buff)
    {
        if (_busterActive == true)
        {
            DisableBuster();
        }
        else
        {
            EnableBuster(buff);
        }
    }

}
