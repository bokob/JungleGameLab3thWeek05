using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskAttack_Y : Node_Y
{
    private Animator _animator;
    private Transform _transform; // 방향 체크용으로 추가
    private Transform _lastTarget;
    private SpriteRenderer _spriteRenderer; // 플립용

    private EnemyManager _enemyManager;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;
    private Transform _staff; // 지팡이 오브젝트

    private float _castingTime = 3f;     // 캐스팅 시간 0.5초
    private float _castingCounter = 0f;    // 캐스팅 카운터
    private bool _isCasting = false;

    private GameObject _fireballPrefab;    // 코드로 로드할 프리팹
    private Quaternion _originalStaffRotation; // 원래 지팡이 각도 저장


    public TaskAttack_Y(Transform transform)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();

        // Resources 폴더에서 파이어볼 프리팹 로드
        _fireballPrefab = Resources.Load<GameObject>("Fireball");
        _spriteRenderer = transform.GetComponent<SpriteRenderer>(); // 플립용
        _staff = transform.Find("Staff");



    }


    public override NodeState Evaluate()
    {
        

        // 타겟 가져오기 전에 null 체크
        object targetData = GetData("target");
        if (targetData == null)
        {
            _animator.SetBool("IsIdle", true);
            
            state = NodeState.FAILURE;
            return state;
        }

        Transform target = (Transform)targetData;
        if (target == null || !target.gameObject.activeInHierarchy)
        {
            ClearData("target");
            _animator.SetBool("IsIdle", true);
            
            state = NodeState.FAILURE;
            return state;
        }


        // 방향 설정 (플립으로)
        bool facingLeft = target.position.x < _transform.position.x;
        _spriteRenderer.flipX = facingLeft; // 왼쪽 보면 플립

        // 쿨타임 체크
        if (_attackCounter > 0)
        {
            _attackCounter -= Time.deltaTime;
            _animator.SetBool("IsIdle", true); // 쿨타임 중 Idle
            
            state = NodeState.RUNNING;
            return state;
        }

        // 캐스팅 시작
        if (!_isCasting)
        {
            _isCasting = true;
            _castingCounter = _castingTime;
            _animator.SetBool("IsIdle", false);
            
            // 나중에 캐스팅 애니메이션 추가 가능
        }

        // 캐스팅 진행
        _castingCounter -= Time.deltaTime;
        if (_castingCounter <= 0 && _isCasting)
        {
            // 공격 애니메이션 재생 (오른쪽만)
            _animator.SetTrigger("Attack");

            // 지팡이 회전
            if (_staff != null)
            {
                _staff.rotation = Quaternion.Euler(0, 0, facingLeft ? 90 : -90);
            }

            // 파이어볼 발사
            GameObject fireball = GameObject.Instantiate(_fireballPrefab, _transform.position, Quaternion.identity);
            Fireball fbScript = fireball.GetComponent<Fireball>();
            Vector2 direction = (target.position - _transform.position).normalized;
            fbScript.SetDirection(direction);

            // 쿨타임 시작
            _attackCounter = _attackTime;
            _isCasting = false;
           

            // 발사 직후 지팡이 원래 각도로 복귀
            //if (_staff != null)
            //{
            //    _staff.rotation = _originalStaffRotation;
            //}
        }

        _animator.SetBool("IsIdle", true); // 공격 후 Idle
        state = NodeState.RUNNING;
        return state;
    }
}
