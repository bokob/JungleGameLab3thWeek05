using System.Collections.Generic;
using static Define;
using UnityEngine;

// and 논리 게이트처럼 작동하는 복합 노드
// 자식 노드를 순서대로 실행하기 위한 노드
// 자식 노드 중 하나가 failure, running을 반환하면 즉시 그 결과를 부모 노드에 즉시 반환
public class Sequence : Node
{
    public Sequence() : base() { }
    public Sequence(List<Node> children) : base(children) { }

    public override NodeState Evaluate()
    {
        bool anyChildRunning = false;

        // TODO: 테스트 주석
        foreach (Node node in children)
        {
            switch (node.Evaluate())
            {
                case NodeState.Failure:
                    nodeState = NodeState.Failure;
                    return nodeState;
                case NodeState.Success:
                    continue;
                case NodeState.Running:
                    anyChildRunning = true;
                    continue;
                default:
                    nodeState = NodeState.Failure;
                    return nodeState;
            }
        }

        nodeState = anyChildRunning ? NodeState.Running : NodeState.Success;
        return nodeState;
    }
}