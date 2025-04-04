using UnityEngine;
using static Define;

public class CheckEnemyInAttackRange_Archer : Node
{
    Transform _transform;
    Animator _anim;

    public CheckEnemyInAttackRange_Archer(Transform transform)
    {
        _transform = transform;
        _anim = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        object targetObject = GetData("target");
        if (targetObject == null)
        {
            nodeState = NodeState.Failure;
            return nodeState;
        }

        Transform target = (Transform)targetObject;
        if (Vector3.Distance(_transform.position, target.position) <= ArcherBT_Archer.AttackRange)
        {
            nodeState = NodeState.Success;
            return nodeState;
        }

        nodeState = NodeState.Failure;
        return nodeState;
    }
}