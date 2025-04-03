using UnityEngine;
using static Define;

public class TaskDash : Node
{
    Transform _transform;
    Animator _anim;
    Status _enemy;
    Dash _dash;
    Rigidbody2D _rb;

    float _dashTime = 3f;
    float _dashCounter = 0f;

    public TaskDash(Transform transform)
    {
        _transform = transform;
        _rb = transform.GetComponent<Rigidbody2D>();
        _anim = transform.GetComponent<Animator>();
        _enemy = transform.GetComponent<Status>();
        _dash = new Dash();
    }

    public override NodeState Evaluate()
    {
        object targetObject = GetData("target");
        if(targetObject == null)
        {
            nodeState = NodeState.Failure;
            return nodeState;
        }

        _dashCounter += Time.deltaTime;

        if(_dashCounter >= _dashTime)
        {
            _dash.Play(_rb, Vector2.up);
            _dashCounter = 0f;
        }
        else
        {
            nodeState = NodeState.Failure;
            return nodeState;
        }
        nodeState = NodeState.Running;
        return nodeState;
    }
}