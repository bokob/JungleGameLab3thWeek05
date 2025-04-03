using UnityEngine;
using static Define;

public class TaskTeleport_Y : Node
{
    Transform _transform;
    float _teleportDistance = 2f; // 텔레포트로 이동할 거리

    public TaskTeleport_Y(Transform transform)
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

        // 적 반대 방향으로 텔레포트
        Vector2 direction = (_transform.position - target.position).normalized;
        Vector3 teleportPosition = _transform.position + (Vector3)direction * _teleportDistance;

        _transform.position = teleportPosition;
        Debug.Log("텔레포트! 새 위치: " + teleportPosition);

        nodeState = NodeState.Success;
        return nodeState;
    }
}