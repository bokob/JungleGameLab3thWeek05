using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckEnemyInFOVRange_Y : Node_Y
{
    private static int _enemyLayerMask = 1 << 6;

    private Transform _transform;
    private Animator _animator;

    public CheckEnemyInFOVRange_Y(Transform transform)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
    }

    //public override NodeState Evaluate()
    //{
    //    object t = GetData("target");
    //    if (t == null)
    //    {
    //        Collider2D[] colliders = Physics2D.OverlapCircleAll
    //            (_transform.position, WizardBT.fovRange, _enemyLayerMask);

    //        if (colliders.Length > 0)
    //        {
    //            // 가장 가까운 적 찾기
    //            Transform closestTarget = null;
    //            float minDistance = float.MaxValue; // 아주 큰 값으로 시작

    //            foreach (Collider2D collider in colliders)
    //            {
    //                float distance = Vector2.Distance(_transform.position, collider.transform.position);
    //                if (distance < minDistance)
    //                {
    //                    minDistance = distance;
    //                    closestTarget = collider.transform;
    //                }
    //            }

    //            // 가장 가까운 적을 타겟으로 설정
    //            parent.parent.SetData("target", closestTarget);
    //            //_animator.SetBool("Walking", true);
    //            state = NodeState.SUCCESS;
    //            return state;

    //            //parent.parent.SetData("target", colliders[0].transform);
    //            ////_animator.SetBool("Walking", true);
    //            //state = NodeState.SUCCESS;
    //            //return state;
    //        }

    //        state = NodeState.FAILURE;
    //        return state;
    //    }

    //    state = NodeState.SUCCESS;
    //    return state;
    //}

    public override NodeState Evaluate()
    {
        // 시야 안의 모든 적 확인
        Collider2D[] colliders = Physics2D.OverlapCircleAll(
            _transform.position, WizardBT_Y.fovRange, _enemyLayerMask);

        if (colliders.Length > 0)
        {
            // 가장 가까운 적 찾기
            Transform closestTarget = null;
            float minDistance = float.MaxValue;

            foreach (Collider2D collider in colliders)
            {
                float distance = Vector2.Distance(_transform.position, collider.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestTarget = collider.transform;
                }
            }

            // 타겟 갱신
            parent.parent.SetData("target", closestTarget);
            //_animator.SetBool("Walking", true);
            state = NodeState.SUCCESS;
            return state;
        }

        // 적이 없으면 타겟 지우기
        parent.parent.ClearData("target");
        state = NodeState.FAILURE;
        return state;
    }

}
