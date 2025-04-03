using UnityEngine;
using static Define;

public class TaskAttack : Node
{
    Animator _anim;
    Transform _transform;

    Sword _sword;

    float _attackTime = 1f;
    float _attackCounter = 0f;

    public TaskAttack(Transform transform)
    {
        _transform = transform;
        _anim = transform.GetComponent<Animator>();
        _sword = transform.GetComponentInChildren<Sword>();
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");

        Debug.Log("공격~");

        _attackCounter += Time.deltaTime;
        if (_attackCounter >= _attackTime)
        {
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