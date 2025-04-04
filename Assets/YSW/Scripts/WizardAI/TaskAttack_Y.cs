using UnityEngine;
using static Define;

public class TaskAttack_Y : Node
{
    Animator _anim;
    Transform _transform;
    Transform _staff;
    GameObject _fireballPrefab;
    Staff_Y _staffY;

    float _attackCooldown = 5f;     // 공격 후 쿨타임
    float _attackCounter = 0f;      // 쿨타임 카운터
    float _castingTime = 10f;        // 캐스팅 시간 (2초)
    float _castingCounter = 0f;     // 캐스팅 진행 카운터
    bool _isCasting = false;        // 캐스팅 중인지 여부

    public TaskAttack_Y(Transform transform)
    {
        _transform = transform;
        _anim = transform.GetComponent<Animator>();
        _staffY = transform.GetComponentInChildren<Staff_Y>();
        _staff = _transform.Find("Weapon/Staff");
        if (_staff == null)
        {
            Debug.LogError("Staff 오브젝트를 Wizard > Weapon 아래에 추가해주세요!");
        }

        _fireballPrefab = Resources.Load<GameObject>("Fireball");
        if (_fireballPrefab == null)
        {
            Debug.LogError("Fireball 프리팹을 Resources 폴더에서 찾을 수 없어요!");
        }
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
            Debug.Log("타겟이 죽었어요! 캐스팅 취소!");
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
            
            Debug.Log("캐스팅 시작!");
        }

        _castingCounter -= Time.deltaTime;
        if (_castingCounter > 0)
        {
            _anim.SetBool("IsMove", false); // 캐스팅 중 Idle
            Debug.Log("캐스팅 중... 남은 시간: " + _castingCounter);
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
            Debug.Log("파이어볼 발사!");
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
    }
}