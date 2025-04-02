using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Define;

public class TaskAttack : Node
{
    Animator _animator;

    Transform _lastTarget;
    //EnemyManager _enemyManager;

    float _attackTime = 1f;
    float _attackCounter = 0f;

    public TaskAttack(Transform transform)
    {
        _animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        if (target != _lastTarget)
        {
            //_enemyManager = target.GetComponent<EnemyManager>();
            _lastTarget = target;
        }

        _attackCounter += Time.deltaTime;
        if (_attackCounter >= _attackTime)
        {
            bool enemyIsDead = true;
            if (enemyIsDead)
            {
                ClearData("target");
                //_animator.SetBool("Attacking", false);
                //_animator.SetBool("Walking", true);
            }
            else
            {
                _attackCounter = 0f;
            }
        }

        nodeState = NodeState.Running;
        return nodeState;
    }
}