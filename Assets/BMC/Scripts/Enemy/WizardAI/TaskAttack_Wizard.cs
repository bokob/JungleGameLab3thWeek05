using UnityEngine;
using static Define;

public class TaskAttack_Wizard : Node
{
    Animator _anim;
    Transform _transform;
    Transform _staff;
    GameObject _fireballPrefab;
    Staff_Wizard _staffY;

    float _attackCooldown = 1f;     // 공격 후 쿨타임
    float _attackCounter = 0f;      // 쿨타임 카운터
    float _castingTime = 2f;        // 캐스팅 시간 (2초)
    float _castingCounter = 0f;     // 캐스팅 진행 카운터
    bool _isCasting = false;        // 캐스팅 중인지 여부

    CastingBar _castingBar;           // CastingBar 스크립트 참조

    public TaskAttack_Wizard(Transform transform)
    {
        _transform = transform;
        _anim = transform.GetComponent<Animator>();
        _staffY = transform.GetComponentInChildren<Staff_Wizard>();
        _staff = _transform.Find("Weapon/Staff");
        _fireballPrefab = Resources.Load<GameObject>("Fireball");

        // CastingBar 참조 (Slider에 붙은 스크립트)
        Transform canvasTransform = _transform.Find("CastingCanvas");
        if (canvasTransform != null)
            _castingBar = canvasTransform.GetComponentInChildren<CastingBar>();
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        if (target == null)
        {
            ResetCasting(); // 타겟 없으면 캐스팅 초기화
            nodeState = NodeState.Failure;
            return nodeState;
        }

        // 타겟이 죽었는지 확인
        Status targetStatus = target.GetComponent<Status>();
        if (targetStatus != null && targetStatus.IsDead)
        {
            ClearData("target"); // 죽은 타겟 지우기
            ResetCasting();      // 캐스팅 취소
            //Debug.Log("타겟이 죽었어요! 캐스팅 취소!");
            nodeState = NodeState.Failure;
            return nodeState;
        }

        // 쿨타임 체크
        if (_attackCounter > 0)
        {
            _attackCounter -= Time.deltaTime;
            _anim.SetBool("IsMove", false); // Idle
            nodeState = NodeState.Running;
            return nodeState;
        }

        // 캐스팅 진행
        if (!_isCasting)
        {
            _isCasting = true;           // 캐스팅 시작
            _castingCounter = _castingTime;

            if (_castingBar != null)
            {
                _castingBar.Show();
               // Debug.Log("캐스팅 시작 - CastingBar 표시");
            }
        }

        _castingCounter -= Time.deltaTime;
        if (_castingCounter > 0)
        {
            _anim.SetBool("IsMove", false); // 캐스팅 중 Idle

            // 캐스팅 진행률 계산 (0~1) 후 슬라이더 업데이트
            if (_castingBar != null)
            {
                float progress = 1f - (_castingCounter / _castingTime); // 0에서 1로 증가
               // Debug.Log($"캐스팅 진행률: {progress} (남은 시간: {_castingCounter})");
                _castingBar.UpdateCastingBar(progress);
            }
            else
            {
                Debug.LogWarning("CastingBar가 null입니다!");
            }
            //Debug.Log("캐스팅 중... 남은 시간: " + _castingCounter);
            nodeState = NodeState.Running;
            return nodeState;
        }

        // 캐스팅 완료 → 파이어볼 발사
        Vector3 spawnOffset = (target.position - _staff.position).normalized * 0.5f;
        GameObject fireball = GameObject.Instantiate(_fireballPrefab, _staff.position + spawnOffset, Quaternion.identity);
        
        Fireball fbScript = fireball.GetComponent<Fireball>();
        if (fbScript != null)
        {
            Vector2 direction = (target.position - _staff.position).normalized;
            fbScript.SetDirection(direction);
            _staffY.Use();               // 지팡이 애니메이션 시작
            //Debug.Log("파이어볼 발사!");
        }

        _attackCounter = _attackCooldown; // 쿨타임 시작
        ResetCasting();                   // 캐스팅 초기화
        _anim.SetBool("IsMove", false);   // Idle

        nodeState = NodeState.Running;
        return nodeState;
    }

    // 캐스팅 초기화 메서드
    private void ResetCasting()
    {
        _isCasting = false;
        _castingCounter = 0f;
        if (_castingBar != null)
        {
            _castingBar.Hide(); // 캐스팅 끝나면 바 숨기기
        }
    }
}