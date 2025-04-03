using UnityEngine;
using BehaviorTree;

public class TaskTeleport_Y : Node_Y
{
    private Transform _transform;
    private float _teleportDistance = 6f; // 텔레포트 거리
    private float _cooldownTime = 5f;    // 쿨타임 5초
    private float _cooldownCounter = 0f; // 쿨타임 카운터

    public TaskTeleport_Y(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        // 쿨타임이 남아 있으면 실패
        if (_cooldownCounter > 0)
        {
            _cooldownCounter -= Time.deltaTime; // 시간 줄이기
            state = NodeState.FAILURE;
            return state;
        }

        // 텔레포트 실행
        Vector2 randomDirection = Random.insideUnitCircle.normalized; // 2D 기준
        Vector3 teleportPosition = _transform.position + new Vector3(randomDirection.x, randomDirection.y, 0) * _teleportDistance;
        _transform.position = teleportPosition;

        // 쿨타임 시작
        _cooldownCounter = _cooldownTime;

        state = NodeState.SUCCESS; // 텔레포트 성공
        return state;
    }
}