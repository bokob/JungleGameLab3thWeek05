using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskAttack : Node
{
    private Animator _animator;

    private Transform _lastTarget;
    private EnemyManager _enemyManager;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;


    private Transform _transform; // 방향 체크용으로 추가

    public TaskAttack(Transform transform)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
    }

    //public override NodeState Evaluate()
    //{
    //    Transform target = (Transform)GetData("target");
    //    if (target != _lastTarget)
    //    {
    //        _enemyManager = target.GetComponent<EnemyManager>();
    //        _lastTarget = target;
    //    }

    //    _attackCounter += Time.deltaTime;
    //    if (_attackCounter >= _attackTime)
    //    {
    //        bool enemyIsDead = _enemyManager.TakeHit();
    //        if (enemyIsDead)
    //        {
    //            ClearData("target");
    //            //_animator.SetBool("Attacking", false);
    //            //_animator.SetBool("WalkRight", true);
    //        }
    //        else
    //        {
    //            _attackCounter = 0f;
    //        }
    //    }

    //    state = NodeState.RUNNING;
    //    return state;
    //}

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");

        // 타겟이 없으면 Idle 상태로
        if (target == null)
        {
            _animator.SetBool("IsIdle", true);
            state = NodeState.FAILURE;
            return state;
        }

        // 새로운 타겟 설정
        if (target != _lastTarget)
        {
            _enemyManager = target.GetComponent<EnemyManager>();
            _lastTarget = target;
        }

        // 방향 설정
        if (target.position.x < _transform.position.x)
        {
            _animator.SetBool("FacingLeft", true);
        }
        else
        {
            _animator.SetBool("FacingLeft", false);
        }

        // 공격 중에는 Idle 상태
        _animator.SetBool("IsIdle", true);

        // 공격 타이머
        _attackCounter += Time.deltaTime;
        if (_attackCounter >= _attackTime)
        {
            bool enemyIsDead = _enemyManager.TakeHit();
            if (enemyIsDead)
            {
                ClearData("target");
                _attackCounter = 0f; // 초기화
                state = NodeState.SUCCESS; // 적 죽으면 성공
            }
            else
            {
                _attackCounter = 0f; // 다시 공격 준비
                state = NodeState.RUNNING;
            }
            return state;
        }

        state = NodeState.RUNNING;
        return state;
    }

}
