using UnityEngine;

public abstract class Tree : MonoBehaviour
{
    protected Node _root = null;

    protected void Start()
    {
        _root = SetUpTree();
    }



    // 트리 구성
    protected abstract Node SetUpTree();
}