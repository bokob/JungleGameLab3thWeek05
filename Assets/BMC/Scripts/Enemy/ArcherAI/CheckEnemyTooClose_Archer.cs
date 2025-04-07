using UnityEngine;
using static Define;

public class CheckEnemyTooClose_Archer : Node
{
    Transform _transform;

    public CheckEnemyTooClose_Archer(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        object targetObject = GetData("target");
        if (targetObject == null)
        {
            nodeState = NodeState.Failure;
            return nodeState;
        }

        Transform target = (Transform)targetObject;
        float distance = Vector3.Distance(_transform.position, target.position);
        if (distance <= ArcherBT.AttackRange && distance < ArcherBT.MinRange)
        {
            nodeState = NodeState.Success; // 너무 가까우면 성공
            return nodeState;
        }

        nodeState = NodeState.Failure;
        return nodeState;
    }
}