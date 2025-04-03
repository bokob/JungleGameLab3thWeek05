using UnityEngine;
using static Define;

public class TaskDash : Node
{
    Transform _transform;
    Dash _dash;
    Rigidbody2D _rb;

    public TaskDash(Transform transform)
    {
        _transform = transform;
        _rb = transform.GetComponent<Rigidbody2D>();
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

        Vector2 dir = Random.insideUnitCircle.normalized;
        _dash.Play(_rb, dir);
        SetData("isReadyDash", false);

        //bool isCanDash = (bool)GetData("isReadyDash");

        //if (isCanDash)
        //{
            
        //}
        //else
        //{
        //    nodeState = NodeState.Failure;
        //    return nodeState;
        //}
        //nodeState = NodeState.Running;
        //return nodeState;

        nodeState = NodeState.Success;
        return nodeState;
    }
}