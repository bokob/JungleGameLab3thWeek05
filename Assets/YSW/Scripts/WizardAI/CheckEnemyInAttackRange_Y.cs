using UnityEngine;
using static Define;

public class CheckEnemyInAttackRange_Y : Node
{
    Transform _transform;
    Animator _anim;

    public CheckEnemyInAttackRange_Y(Transform transform)
    {
        _transform = transform;
        _anim = transform.GetComponent<Animator>();
    }

    //public override NodeState Evaluate()
    //{
    //    object t = GetData("target");
    //    if (t == null)
    //    {
    //        state = NodeState.FAILURE;
    //        return state;
    //    }

    //    Transform target = (Transform)t;
    //    if (Vector2.Distance(_transform.position, target.position) <= WizardBT_Y.attackRange)
    //    {
    //        //_animator.SetBool("Attacking", true);
    //        _animator.SetBool("WalkRight", false);

    //        state = NodeState.SUCCESS;
    //        return state;
    //    }

    //    state = NodeState.FAILURE;
    //    return state;
    //}

    // 평가 함수
    public override NodeState Evaluate()
    {
        object targetObject = GetData("target");
        if (targetObject == null)
        {
            nodeState = NodeState.Failure;
            return nodeState;
        }

        // 공격 거리에 타겟이 있는지 검사
        Transform target = (Transform)targetObject;
        if (Vector3.Distance(_transform.position, target.position) <= WizardBT_Y.AttackRange)
        {
           // Debug.Log("공격준비");

            nodeState = NodeState.Success;
            return nodeState;
        }

        nodeState = NodeState.Failure;
        return nodeState;
    }

}
