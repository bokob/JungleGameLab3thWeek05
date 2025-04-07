using UnityEngine;
using static Define;

public class CheckEnemyInTeleportRange_Wizard : Node
{
    Transform _transform;
    float _teleportRange = 3; // 텔레포트 발동 거리 (적이 2유닛 이내일 때)

    public CheckEnemyInTeleportRange_Wizard(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        if (target == null)
        {
            nodeState = NodeState.Failure;
            return nodeState;
        }

        // 적과의 거리 계산
        float distance = Vector3.Distance(_transform.position, target.position);
       // Debug.Log("적과의 거리: " + distance);

        // 적이 너무 가까우면 텔레포트 필요
        if (distance <= _teleportRange)
        {
            //Debug.Log("적이 너무 가까워요! 텔레포트 준비!");
            nodeState = NodeState.Success;
            return nodeState;
        }

        // 멀면 텔레포트 필요 없음
        nodeState = NodeState.Failure;
        return nodeState;
    }
}