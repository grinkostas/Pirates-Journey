using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NodeAnimator : MonoBehaviour
{
    [SerializeField] private NodeFX _fx;

    public UnityAction<Node, Node> Swap;
    public UnityAction<Node> Fell;

}
