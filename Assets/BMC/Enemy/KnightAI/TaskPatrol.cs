using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

// 순찰을 지속적으로 실행
public class TaskPatrol : Node
{
    Transform _transform;
    Animator _animator;
    Transform[] _waypoints;

    int _currentWaypointIndex = 0;

    float _waitTime = 1f;       // in seconds
    float _waitCounter = 0f;
    bool _waiting = false;

    public TaskPatrol(Transform transform, Transform[] waypoints)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
        _waypoints = waypoints;
    }

    public override NodeState Evaluate()
    {
        if (_waiting)
        {
            _waitCounter += Time.deltaTime;
            if (_waitCounter >= _waitTime)
            {
                _waiting = false;
                //_animator.SetBool("Walking", true);
            }
        }
        else
        {
            Transform wayPoint = _waypoints[_currentWaypointIndex];
            if (Vector3.Distance(_transform.position, wayPoint.position) < 0.01f)
            {
                _transform.position = wayPoint.position;
                _waitCounter = 0f;
                _waiting = true;

                _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
                //_animator.SetBool("Walking", false);
            }
            else
            {
                _transform.position = Vector3.MoveTowards(_transform.position, wayPoint.position, KnightBT.Speed * Time.deltaTime);
                //_transform.LookAt(wayPoint.position);
            }
        }

        nodeState = NodeState.Running;
        return nodeState;
    }
}