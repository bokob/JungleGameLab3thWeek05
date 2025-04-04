using UnityEngine;
using static Define;

public class CheckCloseEnemy_Archer : Node
{
    Transform _transform;
    Animator _anim;

    public CheckCloseEnemy_Archer(Transform transform)
    {
        _transform = transform;
        _anim = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        object targetObject = GetData("target");
        Transform closeEnemy = null;
        float dist = float.MaxValue;

        foreach (Transform enemy in Manager.Game.SpawnedList)
        {
            if (enemy == _transform)
                continue;

            if (Vector3.Distance(_transform.position, enemy.position) < dist)
            {
                dist = Vector3.Distance(_transform.position, enemy.position);
                closeEnemy = enemy;
            }
        }

        parent.Parent.SetData("target", closeEnemy);
        nodeState = NodeState.Success;
        return nodeState;
    }
}