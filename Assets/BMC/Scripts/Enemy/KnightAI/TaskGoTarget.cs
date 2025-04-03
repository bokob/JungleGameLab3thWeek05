using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Define;

public class TaskGoToTarget : Node
{
    Transform _transform;
    Animator _anim;
    Status _enemy;

    public TaskGoToTarget(Transform transform)
    {
        _transform = transform;
        _anim = transform.GetComponent<Animator>();
        _enemy = transform.GetComponent<Status>();
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        if(target == null)
        {
            _anim.SetBool("IsMove", false);
            nodeState = NodeState.Failure;
            return nodeState;
        }

        if (Vector3.Distance(_transform.position, target.position) > KnightBT.AttackRange)
        {
            Debug.Log("이동");
            _anim.SetBool("IsMove", true);

            Vector2 dir = target.position - _transform.position;
            _enemy.Flip(_transform, dir.x);

            _transform.position = Vector3.MoveTowards(
                _transform.position, target.position, KnightBT.MoveSpeed * Time.deltaTime);
        }
        else
        {
            Debug.Log("멈춤");
            _anim.StopPlayback();
            _anim.SetBool("IsMove", false);
            Debug.Log("이동 불가");
        }

        nodeState = NodeState.Running;
        return nodeState;
    }
}