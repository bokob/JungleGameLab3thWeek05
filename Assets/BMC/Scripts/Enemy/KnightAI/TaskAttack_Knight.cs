using UnityEngine;
using static Define;

public class TaskAttack_Knight : Node
{
    Animator _anim;
    Transform _transform;

    Sword _sword;

    float _attackTime = 1f;
    float _attackCounter = 0f;
    float _knockbackForce = 5f;

    public TaskAttack_Knight(Transform transform)
    {
        _transform = transform;
        _anim = transform.GetComponent<Animator>();
        _sword = transform.GetComponentInChildren<Sword>();
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        //Debug.Log("공격~");
        _attackCounter += Time.deltaTime;
        if (_attackCounter >= _attackTime)
        {
            _anim.SetBool("IsMove", true);
            _sword.Use();

            Vector2 dir = target.position - _transform.position;
            target.GetComponent<Rigidbody2D>().AddForce(dir.normalized * _knockbackForce, ForceMode2D.Impulse);
            target.GetComponent<Status>().TakeDamage(5f);

            _attackCounter = 0f;
        }
        nodeState = NodeState.Running;
        return nodeState;
    }
}