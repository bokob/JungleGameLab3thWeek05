using UnityEngine;
using static Define;

public class TaskTeleport_Y : Node
{
    Transform _transform;
    float _teleportDistance = 10f;    // 텔레포트로 이동할 거리
    float _teleportCooldown = 5f;     // 텔레포트 쿨타임
    float _teleportCounter = 0f;      // 쿨타임 카운터
    float _preTeleportDelay = 1f;     // 텔레포트 전 딜레이 시간 (초 단위)
    float _delayCounter = 0f;         // 딜레이 카운터
    bool _isDelaying = false;         // 딜레이 중인지 여부

    public TaskTeleport_Y(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        // 쿨타임 체크
        if (_teleportCounter > 0)
        {
            _teleportCounter -= Time.deltaTime;
            nodeState = NodeState.Failure;
            return nodeState;
        }

        Transform target = (Transform)GetData("target");
        if (target == null)
        {
            nodeState = NodeState.Failure;
            return nodeState;
        }

        // 선 딜레이 처리
        if (!_isDelaying)
        {
            _isDelaying = true;
            _delayCounter = _preTeleportDelay;
            Debug.Log("텔레포트 준비 중...");
        }

        if (_delayCounter > 0)
        {
            _delayCounter -= Time.deltaTime;
            nodeState = NodeState.Running; // 딜레이 중에는 Running 반환
            return nodeState;
        }

        // 텔레포트 실행
        Vector2 direction = (_transform.position - target.position).normalized;
        Vector3 teleportPosition = _transform.position + (Vector3)direction * _teleportDistance;

        _transform.position = teleportPosition;
        Debug.Log("텔레포트! 새 위치: " + teleportPosition);

        // 쿨타임 시작 및 딜레이 초기화
        _teleportCounter = _teleportCooldown;
        _isDelaying = false;

        nodeState = NodeState.Success;
        return nodeState;
    }
}