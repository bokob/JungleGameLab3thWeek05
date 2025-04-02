using UnityEngine;

public abstract class Tree : MonoBehaviour
{
    Node _root = null;

    protected void Start()
    {
        _root = SetUpTree();
    }

    void Update()
    {
        if (_root != null)
            _root.Evaluate();
    }

    // 트리 구성
    protected abstract Node SetUpTree();
}