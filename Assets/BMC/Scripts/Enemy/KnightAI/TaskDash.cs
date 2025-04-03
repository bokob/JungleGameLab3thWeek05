using UnityEngine;
using static Define;

public class TaskDash : Node
{
    Transform _transform;
    Animator _anim;
    Enemy _enemy;

    float _dashTime = 2f;
    float _dashCounter = 0f;


    public TaskDash(Transform transform)
    {
        _transform = transform;
        _anim = transform.GetComponent<Animator>();
        _enemy = transform.GetComponent<Enemy>();
    }

    public override NodeState Evaluate()
    {
        _dashCounter += Time.deltaTime;

        Transform target = (Transform)GetData("target");
        if (Vector3.Distance(_transform.position, target.position) > KnightBT.AttackRange)
        {
            Debug.Log("이동");
            _anim.SetBool("IsMove", true);
            Vector2 dir = target.position - _transform.position;
            _enemy.Flip(_transform, dir.x);
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