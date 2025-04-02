using UnityEngine;
using BehaviorTree;

public class TaskTeleport : Node
{
    private Transform _transform;
    private float _teleportDistance = 5f; // 5미터 멀리 도망가

    public TaskTeleport(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        // 랜덤한 방향으로 5미터 이동
        Vector2 randomDirection = Random.insideUnitCircle.normalized; // 2D 기준
        Vector3 teleportPosition = _transform.position + new Vector3(randomDirection.x, randomDirection.y, 0) * _teleportDistance;
        _transform.position = teleportPosition;

        state = NodeState.SUCCESS; // 텔레포트 성공
        return state;
    }
}