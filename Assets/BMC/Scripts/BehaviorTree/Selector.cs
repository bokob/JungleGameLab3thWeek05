using UnityEngine;
using System.Collections.Generic;
using static Define;

// 논리 게이트(or) 역할을 하는 노드
// Sequence 노드와 거의 동일하지만, 자식이 성공하거나 제대로 실행되면 결과를 즉시 반환
public class Selector : Node
{
    public Selector() : base() { }
    public Selector(List<Node> children) : base(children) { }
    public override NodeState Evaluate()
    {
        foreach(Node node in children)
        {
            switch(node.Evaluate())
            {
                case NodeState.Failure:
                    continue;
                case NodeState.Success:
                    nodeState = NodeState.Success;
                    return nodeState;
                case NodeState.Running:
                    nodeState = NodeState.Running;
                    return nodeState;
                default:
                    continue;
            }
        }

        nodeState = NodeState.Failure;
        return nodeState;
    }
}