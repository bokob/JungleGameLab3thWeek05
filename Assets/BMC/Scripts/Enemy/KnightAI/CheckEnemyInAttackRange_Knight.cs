using UnityEngine;
using static Define;

// 적이 공격 가능한 거리에 있는지 확인하는 노드 (기사)
public class CheckEnemyInAttackRange_Knight : Node
{
    Transform _transform;
    Animator _anim;

    // 생성자
    public CheckEnemyInAttackRange_Knight(Transform transform)
    {
        _transform = transform;
        _anim = transform.GetComponent<Animator>();
    }

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
        if (Vector3.Distance(_transform.position, target.position) <= KnightBT.AttackRange)
        {
            //Debug.Log("공격준비");
            _anim.SetBool("IsMove", false);

            nodeState = NodeState.Success;
            return nodeState;
        }

        nodeState = NodeState.Failure;
        return nodeState;
    }
}