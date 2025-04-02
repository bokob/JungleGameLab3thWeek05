using UnityEngine;
using static Define;

// 적이 공격 가능한 거리에 있는지 확인하는 노드
public class CheckEnemyInAttackRange : Node
{
    Transform _transform;
    Animator _animator;

    // 생성자
    public CheckEnemyInAttackRange(Transform transform)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
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

        Debug.Log("디버그1");

        // 공격 거리에 타겟이 있는지 검사
        Transform target = (Transform)targetObject;
        if (Vector3.Distance(_transform.position, target.position) <= KnightBT.AttackRange)
        {
            Debug.Log("디버그2");

            //_animator.SetBool("Attacking", true);
            //_animator.SetBool("Walking", false);

            nodeState = NodeState.Success;
            return nodeState;
        }

        nodeState = NodeState.Failure;
        return nodeState;
    }
}