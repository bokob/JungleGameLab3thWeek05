using UnityEngine;
using BehaviorTree;

public class CheckEnemyInTeleportRange : Node
{
    private Transform _transform;
    private float _teleportRange = 0.1f; // 적이 1미터 안에 있으면 텔레포트

    public CheckEnemyInTeleportRange(Transform transform)
    {
        _transform = transform;
    }

    //public override NodeState Evaluate()
    //{
    //    object t = GetData("target");
    //    if (t == null)
    //    {
    //        state = NodeState.FAILURE; // 타겟 없으면 실패
    //        return state;
    //    }

    //    Transform target = (Transform)t;
    //    if (Vector2.Distance(_transform.position, target.position) <= _teleportRange)
    //    {
    //        state = NodeState.SUCCESS; // 적이 너무 가까우면 성공
    //        return state;
    //    }

    //    state = NodeState.FAILURE; // 멀면 실패
    //    return state;
    //}


    public override NodeState Evaluate()
    {
        // 1. 자기 자신의 Transform 체크
        if (_transform == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        // 2. 타겟 데이터 가져오기
        object t = GetData("target");
        if (t == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        // 3. 타겟 Transform 체크
        Transform target = (Transform)t;
        if (target == null || !target.gameObject.activeInHierarchy)
        {
            ClearData("target"); // 유효하지 않은 타겟 데이터 제거
            state = NodeState.FAILURE;
            return state;
        }

        // 4. 거리 계산
        if (Vector2.Distance(_transform.position, target.position) <= _teleportRange)
        {
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }
}