using UnityEngine;
using static Define;

public class TaskTeleport_Y : Node
{
    Transform _transform;
    Animator _animator;               // 애니메이터 참조
    float _teleportDistance = 10f;    // 텔레포트로 이동할 거리
    float _teleportCooldown = 5f;     // 텔레포트 쿨타임
    float _teleportCounter = 0f;      // 쿨타임 카운터
    float _preTeleportDelay = 1f;     // 텔레포트 전 딜레이 시간
    float _delayCounter = 0f;         // 딜레이 카운터
    bool _isDelaying = false;         // 딜레이 중인지 여부
    PolygonCollider2D _polygonCollider; // 경기장 영역 참조
    Transform _fixedTarget;           // 고정된 타겟 참조
    public TaskTeleport_Y(Transform transform)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>(); // Animator 가져오기
        // GameManager에서 PolygonCollider2D 참조
        _polygonCollider = GameObject.FindAnyObjectByType<ArenaGround>().GetComponent<PolygonCollider2D>();
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

            _fixedTarget = (Transform)GetData("target");
            if (_fixedTarget == null)
            {
                nodeState = NodeState.Failure;
                return nodeState;
            }
            _isDelaying = true;
            
            _delayCounter = _preTeleportDelay;
            _animator.SetBool("IsTeleporting", true); // 텔레포트 시작 애니메이션 재생

            Debug.Log("텔레포트 준비 중...");
        }

        if (_delayCounter > 0)
        {
            _delayCounter -= Time.deltaTime;
            nodeState = NodeState.Running;
            return nodeState;
        }
        // 텔레포트 실행 (타겟 상태 무시)
        Vector2 direction = _fixedTarget != null
            ? (_transform.position - _fixedTarget.position).normalized
            : Vector2.up; // 타겟이 null이면 기본 방향(위쪽)으로 설정
        Vector3 teleportPosition = _transform.position + (Vector3)direction * _teleportDistance;

        //// 텔레포트 위치 계산
        //Vector2 direction = (_transform.position - target.position).normalized;
        //Vector3 teleportPosition = _transform.position + (Vector3)direction * _teleportDistance;

        // 영역 내로 제한
        Bounds bounds = _polygonCollider.bounds;
        Vector3 clampedPosition = ClampPositionToBounds(teleportPosition, bounds);

        // 텔레포트 실행
        _transform.position = clampedPosition;
        _animator.SetBool("IsTeleporting", false); // 텔레포트 시작 애니메이션 재생
        Debug.Log("텔레포트! 새 위치: " + clampedPosition);

        // 쿨타임 시작 및 딜레이 초기화
        _teleportCounter = _teleportCooldown;
        _isDelaying = false;

        nodeState = NodeState.Success;
        return nodeState;
    }

    // 텔레포트 위치를 Bounds 내로 제한하는 메서드
    private Vector3 ClampPositionToBounds(Vector3 position, Bounds bounds)
    {
        Vector3 clampedPos = position;
        clampedPos.x = Mathf.Clamp(clampedPos.x, bounds.min.x, bounds.max.x);
        clampedPos.y = Mathf.Clamp(clampedPos.y, bounds.min.y, bounds.max.y);

        // PolygonCollider2D 내부인지 확인
        if (!_polygonCollider.OverlapPoint(clampedPos))
        {
            // 영역 밖이면 가장 가까운 유효 위치로 조정
            Vector2 closestPoint = _polygonCollider.ClosestPoint(clampedPos);
            clampedPos = new Vector3(closestPoint.x, closestPoint.y, clampedPos.z);
        }

        return clampedPos;
    }
}