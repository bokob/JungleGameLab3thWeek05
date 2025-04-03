using UnityEngine;
using static Define;

// 대시 준비 상태를 체크하는 노드
public class CheckDash : Node
{
    Transform _transform;

    float _dashTime = 3f;    // 대시 쿨타임
    float _dashCounter = 0f; // 대시 카운터

    public CheckDash(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        object targetObject = GetData("target");
        Transform target = (Transform)targetObject;

        if(target != null)
        {
            object isReadyDashObject = GetData("isReadyDash");
            if (isReadyDashObject == null)
            {
                SetData("isReadyDash", false);
                nodeState = NodeState.Failure;
                return nodeState;
            }
           
            bool isCanDash = (bool)isReadyDashObject;
            if (isCanDash)
                SetData("isReadyDash", false);

            _dashCounter += Time.deltaTime;
            if (!isCanDash && _dashCounter >= _dashTime)
            {
                SetData("isReadyDash", true);
                _dashCounter = 0f;
                nodeState = NodeState.Success;
                return nodeState;
            }
        }
        nodeState = NodeState.Failure;
        return nodeState;
    }
}