using UnityEngine;
using static Define;

public class CheckEnemyInAttackRange_Wizard : Node
{
    Transform _transform;

    public CheckEnemyInAttackRange_Wizard(Transform transform)
    {
        _transform = transform;
    }

    // 평가 함수
    public override NodeState Evaluate()
    {
        object targetObject = GetData("target");
        if (targetObject == null)
        {
            nodeState = NodeState.Failure;
            return nodeState;
        }

        // 공격 거리에 타겟이 있는지 검사
        Transform target = (Transform)targetObject;
        if (Vector3.Distance(_transform.position, target.position) <= WizardBT.AttackRange)
        {
           // Debug.Log("공격준비");
            nodeState = NodeState.Success;
            return nodeState;
        }
        nodeState = NodeState.Failure;
        return nodeState;
    }
}