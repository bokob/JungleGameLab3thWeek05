using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskGoToTarget_Y : Node_Y
{
    private Transform _transform;

    private Animator _animator;
    private bool _lastFacingLeft;

    public TaskGoToTarget_Y(Transform transform)
    {
        _transform = transform;

        _animator = transform.GetComponent<Animator>();
        _lastFacingLeft = false;
    }

    //public override NodeState Evaluate()
    //{
    //    Transform target = (Transform)GetData("target");

    //    if (Vector2.Distance(_transform.position, target.position) > 0.01f)
    //    {
    //        _transform.position = Vector2.MoveTowards(_transform.position,
    //            target.position, WizardBT.speed * Time.deltaTime);
    //        //_transform.LookAt(target.position);
    //    }

    //    state = NodeState.RUNNING;
    //    return state;
    //}



    //public override NodeState Evaluate() //좌우 walk 애니메이션 추가
    //{
    //    Transform target = (Transform)GetData("target");
    //    Animator animator = _transform.GetComponent<Animator>();
    //    if (Vector2.Distance(_transform.position, target.position) > 0.01f)
    //    {
    //        _transform.position = Vector2.MoveTowards(
    //            _transform.position, target.position, WizardBT.speed * Time.deltaTime);

    //        // 좌우 방향 구분
    //        if (target.position.x < _transform.position.x)
    //        {
    //            animator.SetBool("FacingLeft", true);  // 왼쪽 애니메이션
    //        }
    //        else
    //        {
    //            animator.SetBool("FacingLeft", false); // 오른쪽 애니메이션
    //        }
    //    }
    //    state = NodeState.RUNNING;
    //    return state;
    //}

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        if (target == null)
        {
            _animator.SetBool("IsIdle", true); // 타겟 없으면 멈춤
            state = NodeState.FAILURE;
            return state;
        }

        float distance = Vector2.Distance(_transform.position, target.position);
        if (distance > 0.01f)
        {
            _transform.position = Vector2.MoveTowards(
                _transform.position, target.position, WizardBT_Y.speed * Time.deltaTime);

            float directionDifference = target.position.x - _transform.position.x;
            if (directionDifference < -0.1f)
            {
                _animator.SetBool("FacingLeft", true);
                _lastFacingLeft = true;
            }
            else if (directionDifference > 0.1f)
            {
                _animator.SetBool("FacingLeft", false);
                _lastFacingLeft = false;
            }
            _animator.SetBool("IsIdle", false); // 움직이는 중
            state = NodeState.RUNNING;
            return state;
        }

        // 타겟에 도착하면 멈춤
        _animator.SetBool("IsIdle", true);
        state = NodeState.SUCCESS;
        return state;
    }
}
