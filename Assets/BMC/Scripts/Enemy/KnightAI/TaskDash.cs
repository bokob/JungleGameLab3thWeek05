using UnityEngine;
using static Define;

public class TaskDash : Node
{
    Transform _transform;
    Dash _dash;
    Rigidbody2D _rb;
    float _dashOffset = 1.2f;

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

        Transform target = (Transform)targetObject;
        float dist = Vector2.Distance(_transform.position, target.position);
        
        // 공격 사거리 x 보정값 보다 멀면, 그 대상으로 다가가는 대시
        // 공격 사거리 x 보정값 보다 가까우면, 랜덤 방향으로 대시
        Vector2 dir = Vector2.zero;
        dir = (KnightBT.AttackRange * _dashOffset < dist) ? (target.position - _transform.position).normalized : Random.insideUnitCircle.normalized;
        _dash.Play(_rb, dir);
        SetData("isReadyDash", false);
        Debug.Log("대시 사용");

        nodeState = NodeState.Success;
        return nodeState;
    }
}