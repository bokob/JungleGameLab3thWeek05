using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskAttack : Node
{
    private Animator _animator;
    private Transform _transform; // 방향 체크용으로 추가
    private Transform _lastTarget;

    private EnemyManager _enemyManager;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;

    private float _castingTime = 3f;     // 캐스팅 시간 0.5초
    private float _castingCounter = 0f;    // 캐스팅 카운터
    private bool _isCasting = false;

    private GameObject _fireballPrefab;    // 코드로 로드할 프리팹

    public TaskAttack(Transform transform)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();

        // Resources 폴더에서 파이어볼 프리팹 로드
        _fireballPrefab = Resources.Load<GameObject>("Fireball");
        if (_fireballPrefab == null)
        {
            Debug.LogError("Fireball 프리팹을 Resources 폴더에서 찾을 수 없어요!");
        }
    }



    //public override NodeState Evaluate()
    //{
    //    Transform target = (Transform)GetData("target");

    //    // 타겟이 없으면 Idle 상태로
    //    if (target == null)
    //    {
    //        _animator.SetBool("IsIdle", true);
    //        state = NodeState.FAILURE;
    //        return state;
    //    }

    //    // 새로운 타겟 설정
    //    if (target != _lastTarget)
    //    {
    //        _enemyManager = target.GetComponent<EnemyManager>();
    //        _lastTarget = target;
    //    }

    //    // 방향 설정
    //    if (target.position.x < _transform.position.x)
    //    {
    //        _animator.SetBool("FacingLeft", true);
    //    }
    //    else
    //    {
    //        _animator.SetBool("FacingLeft", false);
    //    }

    //    // 공격 중에는 Idle 상태
    //    _animator.SetBool("IsIdle", true);

    //    // 공격 타이머
    //    _attackCounter += Time.deltaTime;
    //    if (_attackCounter >= _attackTime)
    //    {
    //        bool enemyIsDead = _enemyManager.TakeHit();
    //        if (enemyIsDead)
    //        {
    //            ClearData("target");
    //            _attackCounter = 0f; // 초기화
    //            state = NodeState.SUCCESS; // 적 죽으면 성공
    //        }
    //        else
    //        {
    //            _attackCounter = 0f; // 다시 공격 준비
    //            state = NodeState.RUNNING;
    //        }
    //        return state;
    //    }

    //    state = NodeState.RUNNING;
    //    return state;
    //}

    public override NodeState Evaluate()
    {
        //Transform target = (Transform)GetData("target");
        //if (target == null)
        //{
        //    _animator.SetBool("IsIdle", true);
        //    state = NodeState.FAILURE;
        //    return state;
        //}

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


        // 방향 설정
        if (target.position.x < _transform.position.x)
            _animator.SetBool("FacingLeft", true);
        else
            _animator.SetBool("FacingLeft", false);

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
            // 파이어볼 발사
            GameObject fireball = GameObject.Instantiate(_fireballPrefab, _transform.position, Quaternion.identity);
            Fireball fbScript = fireball.GetComponent<Fireball>();
            Vector2 direction = (target.position - _transform.position).normalized;
            fbScript.SetDirection(direction);

            // 쿨타임 시작
            _attackCounter = _attackTime;
            _isCasting = false;
            _lastTarget = target;
        }

        _animator.SetBool("IsIdle", true); // 공격 후 Idle
        state = NodeState.RUNNING;
        return state;
    }
}
