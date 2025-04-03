using UnityEngine;
using static Define;

public class TaskAttack : Node
{
    Animator _anim;
    Transform _transform;
    Transform _lastTarget;
    Rigidbody2D _rb;

    Sword _sword;

    float _attackTime = 1f;
    float _attackCounter = 0f;

    public TaskAttack(Transform transform)
    {
        _transform = transform;
        _anim = transform.GetComponent<Animator>();
        _rb = transform.GetComponent<Rigidbody2D>();
        _sword = transform.GetComponentInChildren<Sword>();
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");

        Debug.Log("공격~");

        if (target != _lastTarget)
        {
            //_enemyManager = target.GetComponent<EnemyManager>();
            _lastTarget = target;
        }

        _attackCounter += Time.deltaTime;
        if (_attackCounter >= _attackTime)
        {
            //ClearData("target");
            _anim.SetBool("IsAttack", false);
            _anim.SetBool("IsMove", true);
            
            _sword.Use();
            Debug.Log("공격?");

            Vector2 dir = target.position - _transform.position;
            target.GetComponent<Rigidbody2D>().AddForce(dir.normalized, ForceMode2D.Impulse);
            target.GetComponent<Status>().TakeDamage(10f);

            _attackCounter = 0f;
        }

        nodeState = NodeState.Running;
        return nodeState;
    }
}