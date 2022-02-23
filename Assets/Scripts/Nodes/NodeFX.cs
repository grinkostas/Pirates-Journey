using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FxSettings
{
    public AnimationCurve Curve;
    public float Duration;
}
public class NodeFX : MonoBehaviour
{
    [SerializeField] private FxSettings _nodeMove;
    [SerializeField] private FxSettings _nodeDestoyScale;
    [SerializeField] private FxSettings _nodeDestoyMove;

    [SerializeField] private GoalBoard _goalBoard;
    [SerializeField] private Board _board;
    [SerializeField] private float _defaultNodeScale = 0.7f;


    public float DestoyAnimationTime => _nodeDestoyMove.Duration;

    delegate void ActionRef(Vector3 delta, Vector3 startPosition, float time, ref float progress);

    public void Move(Node node, Vector3 destination)
    {
        StartCoroutine(MoveAnimation(node, destination));
    }

    
    public IEnumerator MoveAnimation(Node node, Vector3 destination)
    {
        yield return ChangeLocalPositionAnimation(node, destination, _nodeMove);
    }

    public IEnumerator DestroyAnimation(Node node)
    {
        var goalView = _goalBoard.Find(node);
        
        if (goalView != null && node.Buff == null)
        {
            if (goalView.Goal.Received == false)
            {
                if(node.Background != null)                
                    Destroy(node.Background.gameObject);
                
                node.SpriteRenderer.sortingOrder = 5;
                yield return ChangePositionAnimation(node, goalView.transform.position, _nodeDestoyMove);
                yield break;
            }
        }

        yield return DestroyNode(node);


    }

    private IEnumerator DestroyNode(Node node)
    {
        float waitTime = _nodeDestoyScale.Duration;
        if (node.Buff != null)
        {
            if (node.Buff.EffectsDuration > waitTime)            
                waitTime = node.Buff.EffectsDuration;
            node.Buff.Activate();
        }

        StartCoroutine(ZoomOut(node));
        yield return new WaitForSeconds(waitTime);

    }
    public IEnumerator ZoomIn(Node node)
    {
        yield return ChangeScaleAnimation(node, Vector3.one * _defaultNodeScale, _nodeDestoyScale);
    }

    public IEnumerator ZoomOut(Node node)
    {
        yield return ChangeScaleAnimation(node, Vector3.zero, _nodeDestoyScale);
    }

    public IEnumerator DestroyAnimation(Node node, Vector3 destination)
    {
        
        yield return ChangeLocalPositionAnimation(node, destination, _nodeMove);
    }

    private IEnumerator ChangeLocalPositionAnimation(MonoBehaviour objectToAnimate, Vector3 destinationPostion, FxSettings fx)
    {
        if (objectToAnimate == null)        
            yield break;
        
        Vector3 startPosition = objectToAnimate.transform.localPosition;
        Vector3 delta = destinationPostion - startPosition;
        float time = 0;
        float progress = 0;
        while (progress <= 1)
        {
            if (objectToAnimate == null)            
                yield break;
            
            time += Time.deltaTime;
            progress = time / fx.Duration;
            Vector3 currentChange = delta * fx.Curve.Evaluate(progress);
            objectToAnimate.transform.localPosition = startPosition + currentChange;

            yield return null;
        }
    }

    private IEnumerator ChangePositionAnimation(MonoBehaviour objectToAnimate, Vector3 destinationPostion, FxSettings fx)
    {
        if (objectToAnimate == null)
            yield break;

        Vector3 startPosition = objectToAnimate.transform.position;
        Vector3 delta = destinationPostion - startPosition;
        float time = 0;
        float progress = 0;
        while (progress <= 1)
        {
            if (objectToAnimate == null)
                yield break;

            time += Time.deltaTime;
            progress = time / fx.Duration;
            Vector3 currentChange = delta * fx.Curve.Evaluate(progress);
            objectToAnimate.transform.position = startPosition + currentChange;

            yield return null;
        }
    }


    private IEnumerator ChangeScaleAnimation(MonoBehaviour objectToAnimate, Vector3 destinationScale, FxSettings fx)
    {
        if (objectToAnimate == null)
            yield break;

        Vector3 startScale = objectToAnimate.transform.localScale;
        Vector3 delta = destinationScale - startScale;
        float time = 0;
        float progress = 0;
        while (progress <= 1)
        {
            if (objectToAnimate == null)
                yield break;

            time += Time.deltaTime;
            progress = time / fx.Duration;
            Vector3 currentChange = delta * fx.Curve.Evaluate(progress);
            objectToAnimate.transform.localScale = startScale + currentChange;

            yield return null;
        }
    }

}
