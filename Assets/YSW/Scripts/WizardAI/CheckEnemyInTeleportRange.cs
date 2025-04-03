using UnityEngine;
using BehaviorTree;

public class CheckEnemyInTeleportRange : Node
{
    private Transform _transform;
    private float _teleportRange = 1f; // 적이 1미터 안에 있으면 텔레포트

    public CheckEnemyInTeleportRange(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t == null)
        {
            state = NodeState.FAILURE; // 타겟 없으면 실패
            return state;
        }

        Transform target = (Transform)t;
        if (Vector3.Distance(_transform.position, target.position) <= _teleportRange)
        {
            state = NodeState.SUCCESS; // 적이 너무 가까우면 성공
            return state;
        }

        state = NodeState.FAILURE; // 멀면 실패
        return state;
    }
}